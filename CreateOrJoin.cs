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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Runtime.CompilerServices;

namespace TienLenDoAn
{
    
    public partial class CreateOrJoin : Form
    {
        TcpClient client;
        IPEndPoint serverIP; // địa chỉ IP của server
        StreamReader sr;
        StreamWriter sw;
        Packet this_client_info = new Packet();
        string CreateOrJoin1 = " ";
        string RoomID = "";
        string Code = "";
        bool RoomOwner = false;
        byte[] Avatar;
        string Name = "";
        public CreateOrJoin(string configPath)
        {
            InitializeComponent();
            StreamReader streamReader = new StreamReader(configPath);
            string Name1 = streamReader.ReadLine();
            byte[] Avatar1 = converterDemo(Image.FromFile(streamReader.ReadLine()));
            Avatar = Avatar1;
            Name = Name1;
        }
        
        private void Form3_Load(object sender, EventArgs e)
        {

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
                        case "12":

                            MessageBox.Show("The room you are looking for was not found. Please check the room ID or try again.",  "Room Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            button3.BackColor = Color.LightSkyBlue;
                            break;
                        case "13":
                            ClientInfo(Code, RoomID, Name, Avatar, tbServerIP.Text, RoomOwner);
                            
                            break;

                    }
                }
            }
            catch 
            {
                client.Close();
            }
        }

         private void button1_Click(object sender, EventArgs e) // button Create New
        { 
            CreateOrJoin1 = "Create";
            button1.BackColor = Color.DarkGray;
            button2.BackColor = Color.LightSkyBlue;
            button3.BackColor = Color.LightSkyBlue;
            this.Invoke(new Action(() =>
            {
                tbRoomID.Text = "";
                tbRoomID.Visible = false;
                lbRoomID.Visible = false;
            }));
        }

        private void button2_Click(object sender, EventArgs e) // button Join Room
        {
            CreateOrJoin1 = "Join";
            button2.BackColor = Color.DarkGray;
            button1.BackColor = Color.LightSkyBlue;
            button3.BackColor = Color.LightSkyBlue;
            this.Invoke(new Action(() =>
            {
               tbRoomID.Visible = true;
               lbRoomID.Visible = true;
            }));

        }
        public bool IPv4IsValid(string ipv4) // hàm check IP
        {
            if (String.IsNullOrWhiteSpace(ipv4)) return false;

            string[] splitValues = ipv4.Split('.');
            if (splitValues.Length != 4) return false;

            byte posNum;
            return splitValues.All(i => byte.TryParse(i, out posNum));
        }
        private void button3_Click(object sender, EventArgs e) // button Start
        {
            if (!IPv4IsValid(tbServerIP.Text))
            {
                MessageBox.Show("Please enter a valid IPv4 address!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button3.BackColor = Color.LightSkyBlue;
                return;
            }

            button3.BackColor = Color.DarkGray;
            if (!string.IsNullOrEmpty(CreateOrJoin1))
            {
                if (CreateOrJoin1 == "Create")
                {
                    Code = "0";
                    RoomOwner = true;
                    ClientInfo(Code, RoomID, Name, Avatar, tbServerIP.Text, RoomOwner);
                }
                else if (CreateOrJoin1 == "Join")
                {
                    
                    if (checkRoomID(tbRoomID.Text))
                    {
                        tbRoomID.Visible = true;
                        
                        Code = "1";
                        if (tbRoomID.Text == "")
                        {
                            MessageBox.Show("Please enter room ID !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            button3.BackColor = Color.LightSkyBlue;
                        }
                        else CheckRoom();
                    }
                    else 
                    {
                        MessageBox.Show("Room ID not correct!", "Notification" , MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button3.BackColor = Color.LightSkyBlue;
                    }

                }
            }             
        }
        private bool checkRoomID(string s) // hàm check RoomID
        {
            if (s.Length != 4)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] > 57 || s[i] < 48)
                    {
                        return false;
                    }
                }
            }
            return true;

        }
        public static byte[] converterDemo(Image x) 
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }
        void CheckRoom() // hàm kết nối đến server để kiểm tra phòng có tồn tại không
        {
            client = new TcpClient();
            serverIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            client.Connect(serverIP);

            NetworkStream ns = client.GetStream();
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);


            Thread listen = new Thread(receiveFromServer);
            listen.IsBackground = true;
            listen.Start();
            RoomID = tbRoomID.Text;
            this_client_info.Code = "12"; // check room
            this_client_info.RoomID = RoomID;
            sendToServer(this_client_info);

        }
        private void ClientInfo(string Code, string RoomID, string Username = "", byte[] Avatar = null , string IP = "", bool RoomOwner = false) // khởi tạo Form2 
        {
           Multiplay  form2 = new Multiplay (Code, RoomID, Username, Avatar, IP, RoomOwner);
            form2.ShowDialog();
        }

        private void tbRoomID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbServerIP_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
           // Application.OpenForms["Form1"].Close();
        }
    }
}
