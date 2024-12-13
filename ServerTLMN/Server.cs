using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using System.Text;
using System.CodeDom.Compiler;
using System.Net.NetworkInformation;
namespace ServerTLMN
{
    public partial class Server : Form
    {

        Dictionary<int, int> SmallestCards = new Dictionary<int, int>(); // chứa vị trí của user và mã lá bài kiểu int 
        private List<string> deck = new List<string>(); // list chứa 52 lá bài
        private List<Room> roomList = new List<Room>(); // list chứa các room 
        private List<User> userList = new List<User>(); // list chứ các user của 1 room
        private TcpListener listener;
        private TcpClient client;
        private Manager Manager; // dùng để cập nhật các user tạo phòng, vào phòng, thoát phòng, ...
        public Server()
        {
            InitializeComponent();
            Manager = new Manager(listView1, RoomCount, UserCount);
        }

        bool stop = false;
        private void buttonListen_Click(object sender, EventArgs e)
        {
            if (stop == false)
            {
                buttonListen.Text = @"Stop";
                buttonListen.BackColor = Color.DarkGray;

                Manager.WriteToLog("Start listening for incoming requests...");
                listener = new TcpListener(IPAddress.Any, 9999); // kết nối ở port 9999
                listener.Start();

                Thread clientThread = new Thread(Listen);
                clientThread.IsBackground = true;
                clientThread.Start();
                stop = true;
            }
            else
            {
                buttonListen.Text = @"Start";
                buttonListen.BackColor = Color.LightSkyBlue;
                Manager.WriteToLog("Stop listening for incoming requests");
                foreach (User user in userList)
                {
                    user.Client.Close();
                }
                listener.Stop();
                stop = false;
            }
        }
        private void Listen()
        {
            try
            {
                while (true)
                {
                    client = listener.AcceptTcpClient();

                    Thread receiver = new Thread(Receive);
                    receiver.IsBackground = true;
                    receiver.Start(client);
                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
        }
        private void Receive(object obj)
        {
            TcpClient client = obj as TcpClient;
            User user = new User(client);
            userList.Add(user); // thêm người dùng vào userList để quản lý

            try
            {
                string responseInJson = string.Empty;
                while (true)
                {
                    responseInJson = user.Reader.ReadLine();
                    Packet request = JsonConvert.DeserializeObject<Packet>(responseInJson);
                    user.Username = request.Username;
                    if (user.Username == null) userList.Remove(user); // xóa khỏi userlist user check room


                    switch (request.Code)
                    {

                        case "0":
                            CreateNewRoom(user, request); //xử lý khi có người tạo phòng
                            break;
                        case "1":
                            JoinRoom(user, request);  // xử lý khi có người vào phòng
                            break;
                        case "2":
                            Start(request);   // xử lý khi người chơi ấn Deal ( bắt đầu ván chơi )
                            break;
                        case "3":
                            PlayedCard(user, request);  // xử lý các lá bài đã đánh 
                            break;
                        case "4":
                            NextTurnAfterPlay(FindRoom(request.RoomID), user); // xác định lượt đánh tiếp theo 
                            break;
                        case "5":
                            NextTurnAfterSkip(FindRoom(request.RoomID), user); // xác định lượt đánh tiếp theo 
                            break;
                        case "6":
                            Finish(user, request); // xử lý khi có người thắng  
                            break;
                        case "7":
                            ReceiveMessage(request); // xử lý khi người dùng gửi tin nhắn
                            break;
                        case "9":
                            ForwardPlayerCards(user, request); // chuyển tiếp các lá bài sau khi ván đấu kết thúc 
                            break;
                        case "10": // xử lý khi người chơi (không phải chủ phòng) thoát khỏi phòng 
                            ExitRoom(user, request);
                            break;
                        case "11": // xử lý khi chủ phòng thoát khỏi phòng => phòng sẽ bị hủy
                            DeleteRoom(user, request);
                            break;
                        case "12":  // kiểm tra phòng có tồn tại hay không
                            CheckRoom(user, request);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }
        Packet packet2 = new Packet();

        Dictionary<string, List<string>> temp = new Dictionary<string, List<string>>(); //  
        private void ForwardPlayerCards(User user, Packet packet) //hàm gửi các lá bài theo ID của từng user để các user có thể cập nhật giao diện khi có người chiến thắng 
        {
            temp.Add(packet.ID, packet.cards);
            Room room = FindRoom(packet.RoomID);
            if (room.userList.Count - 1 == temp.Count) // kiểm tra số lượng user, khi đủ thì sẽ gửi đến từng user 
            {

                packet2.Code = "9";
                packet2.IDAndCards = temp;
                foreach (User u in room.userList)
                {
                    Send(u, packet2);
                }

            }

        }
        private void DeleteRoom(User user, Packet request) // hàm xử lý sau khi chủ phòng thoát khỏi phòng => phòng bị hủy
        { 
            Room room = FindRoom(request.RoomID); 
            foreach (User u in room.userList)
            {
                if (u != user) Send(u, request); // gửi packet thông báo phòng đã bị hủy
            }
            for (int i = 0; i < room.userList.Count; i++)
            {
                Manager.WriteToLog(room.userList[i].Username + " disconnected."); // lần lượt cập nhật lên server từng user thoát phòng
                userList.Remove(room.userList[i]); // xóa lần lượt các user này khỏi userlist
            }
            room.userList.Clear(); // xóa các user trong userList của phòng 
           

            roomList.Remove(room); // xóa phòng đó khỏi roomList
            Manager.WriteToLog("Deleted room: " + room.roomID + " - No user here."); // cập nhật phòng bị xóa sau khi tất cả user đã thoát 

            Manager.UpdateRoomCount(roomList.Count);   // cập nhật số phòng và số người chơi 
            Manager.UpdateUserCount(userList.Count);  
        }
        private void ExitRoom(User user, Packet request) // hàm xử lý sau khi 1 user thoát khỏi phòng (trừ chủ phòng)
        {
            Room room = FindRoom(request.RoomID); // tìm phòng
            foreach (User u in room.userList)
            {
                if (u != user) Send(u, request); // gửi packet chứa thông tin của user thoát khỏi phòng để các user khác cập nhật
            }
            room.userList.Remove(user); // xóa user đó khỏi userList của phòng

            userList.Remove(user);  // xóa user đó khỏi userList
            Manager.WriteToLog(user.Username + " disconnected."); // server cập nhật user đó thoát phòng
           
            Manager.UpdateRoomCount(roomList.Count);   // cập nhật số phòng và số người chơi 
            Manager.UpdateUserCount(userList.Count);
        }
        private void CheckRoom(User user, Packet request) // hàm kiểm tra xem phòng có tồn tại hay không
        {
            Room room = FindRoom(request.RoomID);
            if (room == null) request.Code = "12"; // nếu phòng không tồn tại
            else request.Code = "13";

            Send(user, request);
            userList.Remove(user);  // do user này được gửi từ Form3 (Form Create New & Join Room nên user phải xóa user này đi )
            Manager.UpdateRoomCount(roomList.Count);   // cập nhật số phòng và số người chơi 
            Manager.UpdateUserCount(userList.Count);
        }
        private void ReceiveMessage(Packet request) // Hàm nhận tin nhắn từ 1 người chơi và gửi đến các người chơi khác 
        {
            Room room = FindRoom(request.RoomID);  // tìm phòng theo RoomID trong packet
            foreach (User u in room.userList)     // duyệt từng user trong phòng  
            {
                if (u.ID != request.ID) Send(u, request);   // chuyển tiếp packet đến các user khác, trừ user đã gửi packet đến server
            }
        }

        private void Finish(User user, Packet request) // Hàm xử lý khi có người thắng
        {
            temp.Clear();
            Room room = FindRoom(request.RoomID);   // tìm phòng theo RoomID trong packet

            Packet packet = new Packet();        // tạo 1 packet mới
            packet.Code = "6";                  //  gán code = "6" và ID vào packet
            packet.ID = request.ID;

            foreach (User u in room.userList)  // duyệt từng user trong phòng 
            {
                if (u.ID != user.ID) Send(u, packet);     // gửi cho packet trên cho các user khác, trừ user đã gửi packet đến server
            }
        }

        private void AddInfo(User user, Packet request)  // Hàm gán các thông tin của packet nhận được vào các user
        {
            user.Username = request.Username;   // gán tên 
            user.Avatar = request.ArrayByte;    // gán mảng byte đường dẫn ảnh
            user.ID = request.ID;               // gán ID
        }
        private void Start(Packet request)     //Hàm bắt đầu ván chơi 
        {
            Deck(); // Hàm tạo và trộn bài
            Room room = FindRoom(request.RoomID); // tìm phòng theo RoomID trong packet
            int index = 0;
            foreach (User user in room.userList)  // duyệt từng user trong phòng 
            {
                Packet packet = new Packet();       // tạo 1 packet mới
                packet.Code = "2";                   // gán code = "2"
                packet.RoomID = room.roomID.ToString();   // gán room ID 

                List<string> list = new List<string>();     // list dùng để chứa 13 mã lá bài kiểu string
                List<int> list2 = new List<int>();          // list dùng để chứa 13 mã lá bài kiểu int
                foreach (string card in deck)               // duyệt từng lá bài trong deck
                {
                    list.Add(card);                         // thêm từng lá bài vào list
                    list2.Add(Convert.ToInt32(card));       // thêm từng lá bài vào list2
                    if (list.Count == 13) break;            // khi đủ 13 lá bài thì dừng                         
                }
                packet.cards = list; // gán list vào thuộc tính cards của packet để gửi đến user

                for (int i = 0; i < 13; i++)
                {
                    deck.Remove(list[i]);   // xóa 13 mã lá bài vừa lấy khỏi deck
                }
                list2.Sort();               // sắp xếp tăng dần để lấy lá bài có mã nhỏ nhất
                SmallestCards[list2[0]] = index;  // thêm vị trí của user vào SmallestCards với key là mã lá bài nhỏ nhất 
                Send(user, packet);              // gửi từng packet đến từng user
                index++;
            }

            int temp = Index(SmallestCards);  // gán vị trí của user có lá bài nhỏ nhất trong userList của phòng 

            Packet packet1 = new Packet(); // tạo packet mới
            packet1.Code = "8"; // gán code = "5"

            Send(room.userList[temp], packet1);  // gửi packet1 cho user có lá bài nhỏ nhất

            deck.Clear(); // xóa bài cũ
        }

        private int Index(Dictionary<int, int> SmallestCards)  // hàm tìm vị trí của user có lá bài nhỏ nhất
        {
            int min = 200;
            foreach (int key in SmallestCards.Keys) 
            {
                if (key < min) min = key;
            }
            return SmallestCards[min];  // trả về vị trí 
        }
        private void CreateNewRoom(User user, Packet request)  // hàm xử lý khi có người chơi tạo phòng
        {
            AddInfo(user, request);    // gán các thông tin từ request vào user

            Random r = new Random();  // tạo ngẫu nhiên RoomID 
            int roomID = r.Next(1000, 9999);

            Room newRoom = new Room();   // tạo phòng mới
            newRoom.roomID = roomID;    // gán roomID cho phòng 
            newRoom.userList.Add(user); // thêm user vào userList của phòng

            roomList.Add(newRoom);    // thêm phòng này vào roomList

            request.RoomID = roomID.ToString();   // gán lại RoomID vào request
            Send(user, request);  // gửi lại user ban đầu 

            Manager.WriteToLog(user.Username + " created new room. Room code: " + newRoom.roomID);  // cập nhật lên listview của server
            Manager.UpdateRoomCount(roomList.Count);   // cập nhật số phòng và số người chơi 
            Manager.UpdateUserCount(userList.Count);
        }
        private void JoinRoom1(User user, Packet request)
        {
            Room room = FindRoom(request.RoomID); // tìm phòng theo roomID trong request

            AddInfo(user, request);  // gán các thông tin từ request vào user
            room.userList.Add(user);  // thêm user vào userList của phòng

            Manager.WriteToLog("Room " + request.RoomID + ": " + user.Username + " joined");  // cập nhật lên listview của server
            Manager.UpdateUserCount(userList.Count); // cập nhật số người chơi

            UpdateUserInRoom(user, request);  // cập nhật người trong phòng          
        }
        private void JoinRoom(User user, Packet request)
        {
            Room room = FindRoom(request.RoomID); // tìm phòng theo roomID trong request 
            AddInfo(user, request);  // gán các thông tin từ request vào user
            room.userList.Add(user);  // thêm user vào userList của phòng

            Manager.WriteToLog("Room " + request.RoomID + ": " + user.Username + " joined");  // cập nhật lên listview của server
            Manager.UpdateUserCount(userList.Count); // cập nhật số người chơi
            UpdateUserInRoom(user, request);  // cập nhật người trong phòng
        }


        private void UpdateUserInRoom(User currUser, Packet request) // hàm cập nhật cho các user khi có 1 user mới vào phòng
        {
            Room room = FindRoom(request.RoomID);
            Packet packet = new Packet(); // tạo packet mới vào gán các thông tin của user hiện tại vào packet
            {
                packet.Code = "1";
                packet.Username = currUser.Username;
                packet.ArrayByte = currUser.Avatar;
                packet.ID = currUser.ID;
            }

            foreach (User user in room.userList)  // duyệt từng user trong phòng 
            {
                if (user != currUser)
                {
                    Packet packet1 = new Packet(); // tạo packet mới vào gán các thông tin của từng user khác cho các user hiện tại
                    packet1.Code = "1";
                    packet1.Username = user.Username;
                    packet1.ArrayByte = user.Avatar;
                    packet1.ID = user.ID;
                    Send(currUser, packet1); // gửi packet1 chứa thông tin của từng user khác cho các user hiện tại
                    Send(user, packet);   // gửi packet chứa thông tin của user hiện tại cho các user khác 
                }
            }
        }


        private void Send(User user, Packet info)  // hàm gửi packet đến user 
        {

            string messageInJson = JsonConvert.SerializeObject(info); // chuyển packet sang chuỗi Json
            try
            {
                user.Writer.WriteLine(messageInJson);   // viết chuỗi Json trên vào luồng của user 
                user.Writer.Flush();
            }
            catch (Exception Ex)
            {
                // MessageBox.Show(Ex.Message); // nếu xảy ra lỗi thì hiển thị lỗi lên MessageBox
            }
        }
        private void Deck()  // Hàm tạo bài và trộn bài
        {
            string[] suits = { "1", "4", "3", "2" };  // 4 chất bài đại diện bằng 4 mã  
            // Các giá trị của lá bài
            string[] ranks = { "15", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14" };  // 15 giá trị bài từ 3 -> Heo đại diện bằng 15 mã
            // Kết hợp chất và giá trị để tạo bộ bài
            foreach (string suit in suits) // duyệt các chất
            {
                foreach (string rank in ranks)  // duyệt các giá trị 
                {
                    deck.Add(rank + suit); // Thêm lá bài vào danh sách
                }
            }
            Random random = new Random();

            // Trộn bộ bài sử dụng thuật toán Fisher-Yates (Shuffle)
            for (int i = deck.Count - 1; i > 0; i--)
            {
                // Chọn chỉ số ngẫu nhiên từ 0 đến i
                int j = random.Next(0, i + 1);

                // Hoán đổi deck[i] và deck[j]
                string temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }
        private Room FindRoom(string roomid) // hàm tìm phòng thông qua RoomID
        {
            foreach (Room r in roomList)  // duyệt danh sách phòng
            {
                if (r.roomID.ToString() == roomid)
                {
                    return r; // trả về phòng có RoomID == roomid
                }
            }
            return null;
        }
        private void PlayedCard(User user, Packet request)  // hàm xử lý các lá bài đã đánh 
        {
            Room room = FindRoom(request.RoomID); // tìm phòng theo roomID của request


            Packet packet = new Packet();  // tạo 1 packet mới
            packet.Username = request.Username;   // gán user name 
            packet.Code = "3";                    // gán code = "3"
            packet.cards = request.cards;       // gán các lá bài 

            foreach (User u in room.userList) // duyệt các user trong room 
            {
                if (u != user) Send(u, packet); // chỉ gửi cho các user khác
            }
        }


        private void NextTurnAfterPlay(Room room, User user) // xử lý khi người chơi play
        {
            Packet packet = new Packet();  // tạo packet mới
            packet.Code = "4";              // gán code = "4"

            for (int i = 0; i < room.userList.Count; i++)  // duyệt danh sách user của phòng
            {
                if (user.ID == room.userList[i].ID)    // tìm vị trí của user hiện tại
                {
                    if (i == room.userList.Count - 1)   // trường hợp vị trí của user là user cuối cùng trong danh sách  
                    {                                   // thì lượt tiếp theo là của user có vị trí là 0 trong danh sách 
                        Send(room.userList[0], packet);         // gửi packet đến user có vị trí 0
                        break;
                    }
                    else                               // trường hợp còn lại thì gửi đến user kế tiếp
                    {
                        Send(room.userList[i + 1], packet);  // gửi packet đến user kế tiếp
                        break;
                    }
                }
            }
            skipturn = 0;  // gán lại biến đếm số lần skip = 0
        }
        int skipturn = 0;
        private void NextTurnAfterSkip(Room room, User user) // xử lý khi người chơi skip
        {
            Packet packet = new Packet(); // tạo packet mới
            packet.Code = "4";               // gán code = "4"

            for (int i = 0; i < room.userList.Count; i++)  // duyệt danh sách user của phòng
            {
                if (user.ID == room.userList[i].ID)       // tìm vị trí của user hiện tại
                {
                    if (i == room.userList.Count - 1)     // trường hợp vị trí của user là user cuối cùng trong danh sách  
                    {                                      // thì lượt tiếp theo là của user có vị trí là 0 trong danh sách 
                        Send(room.userList[0], packet);      // gửi packet đến user có vị trí 0
                        break;
                    }
                    else                             // trường hợp còn lại thì gửi đến user kế tiếp
                    {
                        Send(room.userList[i + 1], packet);  // gửi packet đến user kế tiếp
                        break;
                    }
                }
            }
            skipturn++; // tăng số lần skip lên 1
            if (skipturn == room.userList.Count - 1) PlayedCardClear(room); // khi số lần skip = số user trong phòng - 1 (tức là tất cả người chơi đều bỏ lượt)
        }                                          //  thì gọi hàm PlayedCardClear dùng để gửi đến tất cả các user thông điệp xóa các lá bài đã đánh 

        private void PlayedCardClear(Room room) // hàm gửi đến user thông điệp update các lá bài đá đánh ở giữa form sau khi bỏ lượt
        {
            Packet packet = new Packet(); // tạo 1 packet mới
            packet.Code = "5";             // gán code = "9"
            foreach (User u in room.userList)  // duyệt các user trong room 
            {
                Send(u, packet);  // chỉ gửi cho các user khác
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Server_Load(object sender, EventArgs e)
        {

        }


        private void buttonGetIP_Click(object sender, EventArgs e) // hàm lấy IP
        {
            textBoxIP.Text = Manager.GetLocalIPv4(NetworkInterfaceType.Wireless80211);
        }

        private void Server_Load_1(object sender, EventArgs e)
        {

        }
    }
}