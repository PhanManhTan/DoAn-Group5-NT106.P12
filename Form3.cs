using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TienLenDoAn
{
    public partial class Form3 : Form
    {
        
        string CreateOrJoin = " ";
        string RoomID = "";
        string Code = "";
        bool RoomOwner = false;


        public Form3(string configPath)
        {
            InitializeComponent();
            this.configPath = configPath;
        }
        private string configPath;
        private void Form3_Load(object sender, EventArgs e)
        {
            

        }

    

        private void button1_Click(object sender, EventArgs e)
        {
            CreateOrJoin = "Create";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateOrJoin = "Join";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CreateOrJoin))
            {
                if (CreateOrJoin == "Create")
                {
                    Code = "0";
                    RoomOwner = true;
                    StreamReader streamReader = new StreamReader(configPath);
                    string Name = streamReader.ReadLine();
                    byte[] Avatar = converterDemo(Image.FromFile(streamReader.ReadLine()));

                    ClientInfo(Code, RoomID, Name, Avatar, tbServerIP.Text, RoomOwner);
                }
                else if (CreateOrJoin == "Join")
                {
                    if (checkRoomID(tbRoomID.Text))
                    {
                        tbRoomID.Visible = true;
                        //lbRoomID.Visible = true;

                        Code = "1";
                        if (tbRoomID.Text == "") MessageBox.Show("Please enter room ID !");
                        else RoomID = tbRoomID.Text;
                        StreamReader streamReader = new StreamReader(configPath);
                        string Name = streamReader.ReadLine();
                        byte[] Avatar = converterDemo(Image.FromFile(streamReader.ReadLine()));

                        ClientInfo(Code, RoomID, Name, Avatar, tbServerIP.Text, RoomOwner);

                    }
                    else 
                    {
                        MessageBox.Show("Room ID not correct!");
                    }

                }
            }             
        }
        private bool checkRoomID(string s)
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
        private void ClientInfo(string Code, string RoomID, string Username = "", byte[] Avatar = null , string IP = "", bool RoomOwner = false)
        {
            button1 form2 = new button1(Code, RoomID, Username, Avatar, IP, RoomOwner);
            form2.ShowDialog();
        }

        private void tbRoomID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
