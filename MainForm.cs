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

namespace TienLenDoAn
{
    public partial class TienLenForm : Form
    {
        private string configPath;
        public TienLenForm()
        {
            InitializeComponent();
            // Tạo một mã duy nhất cho mỗi Form1, dùng mã này đặt tên cho file config
            configPath = $".\\Config_{Guid.NewGuid()}.config";
            InitializeConfigFile(); // Tạo file config nếu chưa tồn tại
            this.FormClosing += new FormClosingEventHandler(TienLenForm_FormClosing);
        }
        private void InitializeConfigFile()
        {
            // Kiểm tra nếu file config chưa tồn tại, tạo mới file với nội dung mặc định
            if (!File.Exists(configPath))
            {
                using (StreamWriter writer = new StreamWriter(configPath))
                {
                    writer.WriteLine("Player Name"); // Tên người chơi mặc định
                    writer.WriteLine(".\\Emo\\Avatar\\9.jpg"); // Ảnh đại diện mặc định
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void customButton1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void customButton1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            SinglePlayer singlePlayerForm = new SinglePlayer(this);
            singlePlayerForm.FormClosed += FormChild_FormClosed;
            singlePlayerForm.Show();
        }

        private void customButton4_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        private void customButton3_Click(object sender, EventArgs e)
        {
            Option optionForm = new Option(configPath);
            optionForm.ShowDialog();
        }
        bool multi = false;
        private void MultiplayButton_Click(object sender, EventArgs e)
        {
            if(multi == false)
            {
                this.Hide();
                CreateOrJoin multiplayerForm = new CreateOrJoin(configPath);
                multiplayerForm.FormClosed += FormChild_FormClosed;
                multiplayerForm.Show();
                
            }
            
        }
        private void TienLenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Kiểm tra nếu file config tồn tại thì xóa nó
            if (File.Exists(configPath))
            {
                File.Delete(configPath);
            }
        }
        private void FormChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Hiển thị lại form cha khi form con đóng
            this.Show();
        }
    }

}
