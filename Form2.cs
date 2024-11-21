using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;
using System.Reflection;
using System.CodeDom.Compiler;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;


namespace TienLenDoAn
{
    public partial class button1 : Form
    {
        string User1ID = string.Empty;
        string User2ID = string.Empty;
        string User3ID = string.Empty;
        List<Card> cards = new List<Card>();
        string IPAdd;
        TcpClient client;
        IPEndPoint serverIP; // địa chỉ IP của server
        StreamReader sr;
        StreamWriter sw;
        Packet this_client_info;   // packet chứa thông tin của client 
                                   //   List<Card> ListPlayedCard = new List<Card>(); // Chứa các lá bài gần nhất được đánh ra // dùng để so sánh để biết các lá bài nào được phép đánh

        private List<Card> PlayedCard = new List<Card>();



        private int remaintime = 100;

        private bool iswinner = false;

        public int LoseTimes = 0;

      private Player _Player = new Player();

        private bool PlayerOnTurn = false;

        private List<Card> PreMoveCards = new List<Card>();
        public button1(string Code, string RoomID, string Username, byte[] Avatar, string IP, bool RoomOwner)
        {

            IPAdd = IP;
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            {
                if (Code == "1")
                {
                    textBox2.Text = "Room ID: " + RoomID; // trường hợp người dùng ấy Join Room 
                }
                this_client_info = new Packet();
                this_client_info.Code = Code;
                this_client_info.RoomID = RoomID;
                this_client_info.Username = Username;
                this_client_info.ArrayByte = Avatar;
                this_client_info.ID = CreateID();
            }

            if (!RoomOwner) bTxDeal.Visible = false; //Nếu không phải chủ phòng thì không có button Deal
            You.Text = Username;
            anh1.BackgroundImage = GetImage(Avatar);

        }
        public string CreateID() // hàm tạo ngẫu nhiên ID của user 
        {
            Random random = new Random();
            int code = random.Next(10, 100); // Tạo số ngẫu nhiên từ 10 đến 99
            return code.ToString();
        }

        private Image GetImage(byte[] x) // hàm chuyển đổi mảng byte sang ảnh
        {
            using (MemoryStream ms = new MemoryStream(x))
            {
                return Image.FromStream(ms);
            }

        }
        private void button1_Click(object sender, EventArgs e) // Nút deal
        {


            this_client_info.Code = "2";  // gán code = "2"

            sendToServer(this_client_info);  // gửi packet đến server
            _Player.PlayerCard.Clear(); // xóa các lá bài từ ván trước
            PreMoveCards.Clear();   //


            bTxDeal.Visible = false; // Nút deal   
            PlayerOnTurn = false;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            client = new TcpClient(); // tạo TCP client
            serverIP = new IPEndPoint(IPAddress.Parse(IPAdd), 9999);
            client.Connect(serverIP);
            //  MessageBox.Show("");

            NetworkStream ns = client.GetStream();
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);
            sendToServer(this_client_info);

            Thread listen = new Thread(receiveFromServer);
            listen.IsBackground = true;
            listen.Start();

            panelMess.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
        }
        private void sendToServer(Packet info) // hàm gửi packet đến server
        {
            string messageInJson = JsonConvert.SerializeObject(info);
            try
            {
                sw.WriteLine(messageInJson);
                sw.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void receiveFromServer()
        {
            try
            {
                string requestInJson = string.Empty;
                while (true)
                {
                    requestInJson = sr.ReadLine();

                    Packet request = JsonConvert.DeserializeObject<Packet>(requestInJson);

                    switch (request.Code)
                    {
                        case "0":  // Trả về Room ID sau user tạo phòng mới
                            textBox2.Text = "Room ID: " + request.RoomID.ToString(); // hiện RoomID lên textBox2
                            this_client_info.RoomID = request.RoomID.ToString();  // gán RoomID vào packet 
                            break;
                        case "1": // Sau khi user mới vào 1 phòng, các user khác cập nhật user mới
                            UpdateUserInRoom(request);
                            break;
                        case "2":
                            GetCards(request); // Sau khi chủ phòng ấn Deal, Server sẽ gửi các lá bài đến các user
                            Start(request);  // Hiển thị các phím Play, Unchose All, Skip và 1 lá bài úp lại đại diện cho các lá bài của các user khác
                            break;
                        case "3":
                            DrawPlayedCards(request);   // Vẽ các lá bài đã được đánh ra bởi các người chơi khác trong phòng
                            break;
                        case "4":
                            NextTurn(request); // server cho biết ai là người đánh tiếp theo                    
                            break;
                        case "5":
                            ClearPlayedCards(); // xóa các lá bài đã được đánh ra sau khi skip 
                            break;
                        case "6":
                            Loser(request);//server yêu cầu gửi các lá bài chưa đánh của người thua                            
                            break;
                        case "7":
                            DisplayMessage(request);  // hiện thị tin nhắn khi có người gửi 
                            break;
                        case "8":
                            FirstTurn(request);  // gửi đến user có lá bài nhỏ nhất sau khi chủ phòng ấn Deal
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClearPlayedCards()
        {
            RemovePlayedCard();  // xóa các lá bài đã đánh 
            PlayedCard.Clear();   // xóa list lá bài đã đánh
            PreMoveCards.Clear();  // 
        }
        private void tmrPlayer_Tick(object sender, EventArgs e)
        {
            base.BeginInvoke(new button1.ChangePbrValue(this.ChgPbrValue), new object[]
            {
                this.pbrRemainTime,
                this.remaintime
            });
            if (this.remaintime <= 20)
            {
                if (this.pbxClock.Visible)
                {
                    base.BeginInvoke(new button1.HidePicturebox(this.HdPbx), new object[]
                    {
                        this.pbxClock
                    });
                }
                else
                {
                    base.BeginInvoke(new button1.ShowPicturebox(this.ShwPbx), new object[]
                    {
                        this.pbxClock
                    });
                }
            }
            if (this.remaintime == 0)
            {
                this.cmdSkip.Enabled = true;
                this.cmdSkip.PerformClick();
            }
            else
            {
                this.remaintime--;
            }
        }


        void Start(Packet packet) // hàm hiển thị các button Play, Skip, UnChose và các lá bài tượng trưng cho các người chơi khác. 
        {
            this.Invoke(new Action(() =>
            {
                this.bTxDeal.Visible = false;  //  Ẩn button Deal
                this.cmdPlay.Visible = true;
                this.cmdSkip.Visible = true;
                this.cmdUnChose.Visible = true;
                if (User1.Text != "") pbxLeft.Visible = true;
                if (User2.Text != "") pbxOpponent.Visible = true;
                if (User3.Text != "") pbxRight.Visible = true;
            }));

        }

        void DrawPlayedCards(Packet request)  //hàm vẽ các lá bài của các user đánh ra
        {
            this.PreMoveCards.Clear();
            Card card = new Card();
            for (int i = request.cards.Count - 1; i >= 0; i--)
            {
                card = ConvertCodeToCard(request.cards[i]);
                card.Click -= this.Choseobj_Click;
                this.PreMoveCards.Add(card);
            }
            this.DrawPlayedCard1();
        }
        private void DrawPlayedCard1()
        {
            this.RemovePlayedCard();
            int num = this.PreMoveCards.Count<Card>();
            int num2 = base.Width - (base.Width - (22 * this.PreMoveCards.Count<Card>() + 112)) / 2 - 112;
            for (int i = num - 1; i >= 0; i--)
            {
                Card card = this.PreMoveCards[i];
                this.PlayedCard.Add(card);
                card.Location = new Point(num2, 240);
                card.Click -= this.Choseobj_Click;
                base.BeginInvoke(new button1.AddCardtoForm(this.AddCrdtoForm), new object[]
                {
                    card
                });
                num2 -= 22;
                num = this.PreMoveCards.Count<Card>();
            }
            this.UnClickableCards(this.PlayedCard);
        }

        private void FirstTurn(Packet packet)
        {
            this.Invoke(new Action(() =>
            {

                lblStatus.Visible = true;
                this.lblStatus.Text = "Your turn!";
                PlayerOnTurn = true;

                this.RemovePlayedCard();
                this.PreMoveCards.Clear();
                this.PlayerBeginTurn();

            }));
        }
        private void NextTurn(Packet packet)
        {

            {
                this.Invoke(new Action(() =>
                {
                    lblStatus.Visible = true;
                    this.lblStatus.Text = "Your turn!";
                    PlayerOnTurn = true;
                    this.PlayerBeginTurn();

                }));
            } 
        }
        Card ConvertCodeToCard(string code) // Chuyển các mã bài trong packet sang đối tượng Card
        {

            NameofCard temp;
            TypeofCard temp1;

            Card card = new Card();
            if (code.Length == 3)
            {
                if (Enum.TryParse(code.Substring(2, 1), true, out temp1) && Enum.TryParse(code.Substring(0, 2), true, out temp))
                {
                    card = new Card(temp, temp1);
                    card.Click += Choseobj_Click;
                }
            }
            else
            {
                if (Enum.TryParse(code.Substring(1, 1), true, out temp1) && Enum.TryParse(code.Substring(0, 1), true, out temp))
                {
                    card = new Card(temp, temp1);
                    card.Click += Choseobj_Click;
                }
            }
            return card;  // trả về đối tượng card
        }
        string ConvertCardToCode(Card card) // Chuyển đối tượng Card sang kiểu mã bài để bỏ vào packet gửi đến Server
        {
            string nameString;
            string cardString;
            if (Enum.TryParse(card.TypeOfCard.ToString(), out TypeofCard cardType) && Enum.TryParse(card.NameOfCard.ToString(), out NameofCard cardName))
            {
                cardString = ((int)cardType).ToString();
                nameString = ((int)cardName).ToString();
                return nameString + cardString;  // trả về mã bài
            }
            else return "";

        }
        private void GetCards(Packet request) // hàm nhận bài từ server sau khi chủ phòng ấn Deal
        {
            this.RemovePlayedCard(); // Xóa hết các lá bài đã đánh từ ván trước trên form
            this.RemovePlayerCards(); // Xóa hết các lá bài chưa được đánh từ ván trước trên form
            this._Player.PlayerCard.Clear();  // Xóa các lá bài chưa đánh hết từ ván trước 
            this.PreMoveCards.Clear();   // Xóa các lá bài đã chọn từ ván trước 



            foreach (string item in request.cards)  // duyệt các mã bài trong packet
            {
                Card card = ConvertCodeToCard(item);  // chuyển đổi mã bài sang đối tượng Card
                _Player.PlayerCard.Add(card);  // thêm các lá bài vào phương thức PlayerCard của đối tượng Player

            }
            _Player.SortCList(ref _Player.PlayerCard); // sắp xếp các lá bài theo đúng thứ tự bằng phương thức SortClist
            DrawPlayerCard(ref _Player.PlayerCard);  // gọi hàm vẽ 13 lá bài, hiển thị lên form

            Wait();

            
        }
        void Wait() // hàm chờ đợi
        {
            this.Invoke(new Action(() =>
            {
                lblStatus.Visible = true;
                lblStatus.Text = "Wait for your turn..."; // hiện label Status và hiện thị Wait for your turn .. 
            }));
            this.PlayerOnTurn = false;
        }
        private void UpdateUserInRoom(Packet request) // hàm thêm các user khác vào form của user hiện tại
        {

            if (User1.Text == "")   // kiểm tra user ở bên trái trên form
            {
                this.Invoke(new Action(() => {
                    pBuser1.Visible = true;  // hiện textbox chứa tên, và picturebox chứa ảnh
                    User1.Visible = true;
                }));

                User1.Text = request.Username.ToString();  // gán tên
                pBuser1.Image = GetImage(request.ArrayByte);  // gán ảnh

            }
            else if (User2.Text == "") // kiểm tra user ở bên trên trên form
            {

                this.Invoke(new Action(() => {
                    pBuser2.Visible = true;   // hiện textbox chứa tên, và picturebox chứa ảnh
                    User2.Visible = true;
                }));
                User2.Text = request.Username.ToString(); // gán tên
                pBuser2.Image = GetImage(request.ArrayByte);  // gán ảnh

            }
            else if (User3.Text == "") // kiểm tra user ở bên phải trên form
            {

                this.Invoke(new Action(() => {
                    pBuser3.Visible = true;   // hiện textbox chứa tên, và picturebox chứa ảnh
                    User3.Visible = true;
                }));
                User3.Text = request.Username.ToString();  // gán tên
                pBuser3.Image = GetImage(request.ArrayByte); // gán ảnh
            }
        }

        private void AddCrdtoForm(Card Temp)
        {
            base.Controls.Add(Temp);
        }

        private void DrawPlayerCard(ref List<Card> CardList)   // vẽ các lá bài của người chơi 
        {
            int num = CardList.Count<Card>();
            int num2 = base.Width - (base.Width - (22 * CardList.Count<Card>() + 112)) / 2 - 112;
            for (int i = num - 1; i >= 0; i--)
            {
                Card card = CardList[i];
                card.Location = new Point(num2, 430);
                base.BeginInvoke(new button1.AddCardtoForm(this.AddCrdtoForm), new object[]
                {
                    card
                });
                num2 -= 22;
                num = CardList.Count<Card>();
            }
        }

        private void UnChoseAll()  // hàm hủy chọn tất cả lá bài
        {
            int num = this._Player.PlayerCard.Count<Card>();
            for (int i = 0; i < num; i++)
            {
                if (this._Player.PlayerCard[i].Choose)
                {
                    this._Player.PlayerCard[i].Location = new Point(this._Player.PlayerCard[i].Location.X, this._Player.PlayerCard[i].Location.Y + 25);
                    this._Player.PlayerCard[i].Choose = false;
                    this._Player.ChoseCard.Remove(this._Player.PlayerCard[i]);
                }
            }
            this.cmdPlay.Enabled = false;
            this.cmdUnChose.Enabled = false;
        }


        private void Choseobj_Click(object sender, EventArgs e)
        {
            Card card = sender as Card;
            if (card.Choose)
            {
                this._Player.ChoseCard.Remove(card); // hủy chọn
            }
            else
            {
                this._Player.ChoseCard.Add(card); // chọn
            }
            if (this.PlayerOnTurn)
            {
                if (this._Player.DoCheck(this.PreMoveCards)) // kiểm tra xem bài có đánh đc không 
                {
                    this.cmdPlay.Enabled = true;
                }
                else
                {
                    this.cmdPlay.Enabled = false;
                }
            }
            if (this._Player.ChoseCard.Count != 0)  // kiểm tra có đang chọn bài không để ẩn hiện Unchose
            {
                this.cmdUnChose.Enabled = true;
            }
            else
            {
                this.cmdUnChose.Enabled = false;
            }
            card.Toggle(); // ảnh lá bài khi được chọn 
        }
        private void UnClickableCards(List<Card> List) // bỏ chọn bài
        {
            for (int i = 0; i < List.Count; i++)
            {
                List[i].Click -= this.Choseobj_Click;
            }
        }

        private void RemovePlayedCard() // Loại bài đã đánh //
        {

            if (this.PlayedCard.Count != 0)
            {
                for (int i = 0; i < this.PlayedCard.Count<Card>(); i++)
                {


                    base.BeginInvoke(new button1.RemoveCardfromForm(this.RevCrdfromForm), new object[]
                    {
                        this.PlayedCard[i]
                    });
                }
            }
            this.PlayedCard.Clear();


        }

        private void RemovePlayerCards() // Loại bài của ng chơi
        {

            for (int i = 0; i < base.Controls.Count; i++)
            {
                if (base.Controls[i] is Card)
                {
                    base.BeginInvoke(new button1.RemoveCardfromForm(this.RevCrdfromForm), new object[]
                    {
                        base.Controls[i]
                    });
                }
            }
        }

        private void DrawPlayedCard() // Vẽ bài đã đánh và gửi
        {
            this_client_info.Code = "3";  // gán code "3"
            this_client_info.cards = new List<string>();          // Tạo 1 packet để gửi các lá bài đã đánh đến Server

            this.RemovePlayedCard();  // xóa các lá bài đã đánh trên form
            int num = this.PreMoveCards.Count<Card>();
            int num2 = base.Width - (base.Width - (22 * this.PreMoveCards.Count<Card>() + 112)) / 2 - 112;
            for (int i = num - 1; i >= 0; i--)
            {
                Card card = this.PreMoveCards[i];     // duyệt các lá bài trong danh sách các lá bài đã chọn và vừa đánh
                this.PlayedCard.Add(card);           // thêm vào danh sách các lá bài đã đánh

                this_client_info.cards.Add(ConvertCardToCode(card));  // thêm các lá bài đã đánh vào packet để gửi đến server

                card.Location = new Point(num2, 240);
                card.Click -= this.Choseobj_Click;
                base.BeginInvoke(new button1.AddCardtoForm(this.AddCrdtoForm), new object[]  // thêm các lá bài trên vào giữa màn hình( vị trí các lá bài đã được đánh )
                {
                    card
                });
                num2 -= 22;
                num = this.PreMoveCards.Count<Card>();
            }
            this.UnClickableCards(this.PlayedCard);      // hủy chế độ chọn của các lá bài


            sendToServer(this_client_info);
        }
        private void Winner() // hàm được gọi thì người chơi thắng
        {
            this.bTxDeal.Visible = true;
            base.BeginInvoke(new button1.ChangeLableText(this.ChgLblText), new object[]
            {
                this.lblStatus,   // hiển thị lên label Status You Win
                "You Win!"
            });
            this.iswinner = true;
            this.tmrPlayer.Stop();
        }
        private void Loser(Packet request)
        {
            if (User1ID == request.ID) this.Invoke(new Action(() => pbxLeft.Visible = false));  // ẩn các lá bài đại diện cho các người chơi khác
            if (User2ID == request.ID) this.Invoke(new Action(() => pbxOpponent.Visible = false));
            if (User3ID == request.ID) this.Invoke(new Action(() => pbxRight.Visible = false));

            base.BeginInvoke(new button1.StopTimer(this.StpTimer), new object[] // dừng timer
            {
                this.tmrPlayer
            });
            base.BeginInvoke(new button1.HideProgressbar(this.HdProgressbar), new object[] // ẩn thanh thời gian
            {
                this.pbrRemainTime
            });
            base.BeginInvoke(new button1.HidePicturebox(this.HdPbx), new object[]  // ẩn đồng hồ
            {
                this.pbxClock
            });

            base.BeginInvoke(new button1.HideButton(this.HdBtt), new object[]  // ẩn button Play
            {
                this.cmdPlay
            });
            base.BeginInvoke(new button1.HideButton(this.HdBtt), new object[]   // ẩn button Skip
            {
                this.cmdSkip
            });
            base.BeginInvoke(new button1.EnableButton(this.EnbBtt), new object[]  // enable button Skip
            {
                this.cmdSkip
            });
            base.BeginInvoke(new button1.HideButton(this.HdBtt), new object[]  // ẩn button UnChose
            {
                this.cmdUnChose
            });
            base.BeginInvoke(new button1.ChangeLableText(this.ChgLblText), new object[] // thay đổi label Status thành You Loser
            {
                this.lblStatus,
                "You Lose!"
            });
            this.UnClickableCards(this._Player.PlayerCard); // hủy bỏ chọn các lá bài
            this.iswinner = false;
        }


        private void PlayerBeginTurn()
        {
            this.PlayerOnTurn = true;
            this.remaintime = 100;
            base.BeginInvoke(new button1.ShowProgressbar(this.ShwProgressbar), new object[]
            {
                this.pbrRemainTime
            });
            base.BeginInvoke(new button1.ShowPicturebox(this.ShwPbx), new object[]
            {
                this.pbxClock
            });
            base.BeginInvoke(new button1.ChangeLableText(this.ChgLblText), new object[]
            {
                this.lblStatus,
                "Your turn!"
            });
            base.BeginInvoke(new button1.StartTimer(this.StrTimer), new object[]
            {
                this.tmrPlayer
            });
            if (this._Player.DoCheck(PlayedCard))
            {
                base.BeginInvoke(new button1.EnableButton(this.EnbBtt), new object[]
                {
                    this.cmdPlay
                });
            }
            if (this.PreMoveCards.Count != 0)
            {
                base.BeginInvoke(new button1.EnableButton(this.EnbBtt), new object[]
                {
                    this.cmdSkip
                });
            }
        }



        private void RevCrdfromForm(Card Temp)
        {
            base.Controls.Remove(Temp);
        }


        private void PfmClick(Button Butt)
        {
            Butt.PerformClick();
        }
        private void EnbBtt(Button Butt)
        {
            Butt.Enabled = true;
        }

        private void DsbBtt(Button Butt)
        {
            Butt.Enabled = false;
        }

        private void ShwBtt(Button Butt)
        {
            Butt.Visible = true;
        }

        private void HdBtt(Button Butt)
        {
            Butt.Visible = false;
        }

        private void ShwPbx(PictureBox Pbx)
        {
            Pbx.Visible = true;
        }


        private void HdPbx(PictureBox Pbx)
        {
            Pbx.Visible = false;
        }

        private void ChgLblText(Label Lbl, string s)
        {
            Lbl.Text = s;
        }

        private void StrTimer(System.Windows.Forms.Timer tmr)
        {
            tmr.Start();
        }

        private void StpTimer(System.Windows.Forms.Timer tmr)
        {
            tmr.Stop();
        }
        private void BrgtoFront(Form frm)
        {
            frm.BringToFront();
        }

        private void VsbTrue(Form frm)
        {
            frm.Visible = true;
        }

        private void ChgPbrValue(ProgressBar pbr, int i)
        {
            pbr.Value = i;
        }

        private void ShwProgressbar(ProgressBar pbr)
        {
            pbr.Visible = true;
        }

        private void HdProgressbar(ProgressBar pbr)
        {
            pbr.Visible = false;
        }


        public delegate void AddCardtoForm(Card Temp);
        public delegate void RemoveCardfromForm(Card Temp);
        public delegate void PerformClick(Button Butt);
        public delegate void EnableButton(Button Butt);
        public delegate void DisableButton(Button Butt);
        public delegate void ShowButton(Button Butt);
        public delegate void HideButton(Button Butt);
        public delegate void ShowPicturebox(PictureBox Pbx);
        public delegate void HidePicturebox(PictureBox Pbx);
        public delegate void ChangeLableText(Label Lbl, string s);
        public delegate void StartTimer(System.Windows.Forms.Timer tmr);
        public delegate void StopTimer(System.Windows.Forms.Timer tmr);
        public delegate void ChangePbrValue(ProgressBar pbr, int i);
        public delegate void ShowProgressbar(ProgressBar pbr);
        public delegate void HideProgressbar(ProgressBar pbr);

        private void cmdSkip_Click(object sender, EventArgs e)
        {
            this.tmrPlayer.Stop();    // dừng đếm ngược thời gian
            this.lblStatus.Text = "Wait for your turn...";  // hiển thị lên label 
            this.pbrRemainTime.Visible = false;    // ẩn đồng hồ thời gian
            this.pbxClock.Visible = false;
            this.UnChoseAll();              // 
            this.PreMoveCards.Clear();
            this.PlayerOnTurn = false;       //
            this.cmdPlay.Enabled = false;    // ẩncác button Play, Chose, Skip 
            this.cmdUnChose.Enabled = false;
            this.cmdSkip.Enabled = false;

            this_client_info.Code = "5";
            sendToServer(this_client_info); // gửi đến server packet có code = "5"

        }
        private void cmdPlay_Click(object sender, EventArgs e)
        {
            this.lblStatus.Text = "Wait for your turn...";    // hiển thị lên label 
            this.pbrRemainTime.Visible = false;                // ẩn đồng hồ thời gian
            this.pbxClock.Visible = false;
            this.PlayerOnTurn = false;                         // để enable button Play
            this.PreMoveCards.Clear();
            this.PreMoveCards.AddRange(this._Player.ChoseCard);
            for (int i = 0; i < this._Player.ChoseCard.Count; i++)
            {
                this._Player.PlayerCard.Remove(this._Player.ChoseCard[i]);
            }
            this._Player.ChoseCard.Clear();
            this.DrawPlayedCard();                          // vẽ các lá bài vừa đánh
            this.DrawPlayerCard(ref this._Player.PlayerCard);    // xóa các lá bài vừa đánh trên tay 
            this.cmdPlay.Enabled = false;              //
            this.cmdUnChose.Enabled = false;
            this.cmdSkip.Enabled = false;
            this.HdProgressbar(pbrRemainTime);
            this.pbxClock.Visible = false;
            if (this._Player.PlayerCard.Count == 0)
            {
                this.Winner();
                this_client_info.Code = "6";
            }
            else
            {
                this.tmrPlayer.Stop();
                this_client_info.Code = "4";
                
            }
            sendToServer(this_client_info);  // gửi đến server packet có code = "4"
        }

        private void cmdUnChose_Click(object sender, EventArgs e)
        {
            this.UnChoseAll();
        }


        private void anh1_Click(object sender, EventArgs e)
        {
            if (panelMess.Visible)
            {
                panelMess.Visible = false;
            }
            else
            {
                panelMess.Visible = true;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBoxMess.Text))
            {
                this_client_info.Code = "7";
                this_client_info.ArrayByte = Encoding.UTF8.GetBytes(richTextBoxMess.Text);

                sendToServer(this_client_info);
                richTextBoxMess.Text = string.Empty;
                panelMess.Visible = false;
            }
        }
        private void DisplayMessage(Packet packet)
        {
            // Timer để hiển thị tin nhắn trong 4 giây
            var displayTimer = new System.Threading.Timer(state =>
            {
                // Cập nhật UI để hiển thị tin nhắn từ client khác
                this.Invoke((MethodInvoker)(() =>
                {
                    string messageText = Encoding.UTF8.GetString(packet.ArrayByte);

                    if (User1.Text == packet.Username)
                    {
                        panel1.Visible = true;
                        richTextBoxU1.Visible = true;
                        richTextBoxU1.Text = messageText;
                    }
                    else if (User2.Text == packet.Username)
                    {
                        panel2.Visible = true;
                        richTextBoxU2.Visible = true;
                        richTextBoxU2.Text = messageText;
                    }
                    else if (User3.Text == packet.Username)
                    {
                        panel3.Visible = true;
                        richTextBoxU3.Visible = true;
                        richTextBoxU3.Text = messageText;
                    }
                }));

                // Sau 4 giây, ẩn tin nhắn đi
                var hideTimer = new System.Threading.Timer(_ =>
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        if (User1.Text == packet.Username)
                        {
                            richTextBoxU1.Clear(); // Xóa nội dung tin nhắn
                            richTextBoxU1.Visible = false;
                            panel1.Visible = false;
                        }
                        else if (User2.Text == packet.Username)
                        {
                            richTextBoxU2.Clear();
                            richTextBoxU2.Visible = false;
                            panel2.Visible = false;
                        }
                        else if (User3.Text == packet.Username)
                        {
                            richTextBoxU3.Clear();
                            richTextBoxU3.Visible = false;
                            panel3.Visible = false;
                        }
                    }));
                }, null, 3000, Timeout.Infinite); // Đặt 4 giây cho timer ẩn
            }, null, 0, Timeout.Infinite);
        }

        // Phương thức này sẽ được gọi bởi thread nhận tin nhắn khi nhận được tin nhắn từ client khác
        private void OnMessageReceived(Packet packet)
        {
            // Gọi DisplayMessage để hiển thị tin nhắn nhận được
            this.Invoke((MethodInvoker)(() => DisplayMessage(packet)));
        }

        private void richTextBoxMess_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void pbxClock_Click(object sender, EventArgs e)
        {

        }

        private void cmdDeal_Click(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void pbrRemainTime_Click(object sender, EventArgs e)
        {

        }

        private void tmrDownRight_Tick(object sender, EventArgs e)
        {

        }

    }
}
