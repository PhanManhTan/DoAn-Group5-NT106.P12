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
    public partial class Option : Form
    {
        public Option(Form frm)
        {
            InitializeComponent();
        }
        private string avatarpath;
        private string playername;
        private string configPath;

        private void focusTxtBox()
        {
            this.txtPlyerName.Focus();
        }

        public Option(string configPath)
        {
            InitializeComponent();
            this.configPath = configPath; // Nhận đường dẫn config từ Form1
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPlyerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void Option_Load(object sender, EventArgs e)
        {
            // Đọc từ file config
            using (StreamReader streamReader = new StreamReader(configPath))
            {
                this.playername = streamReader.ReadLine();
                this.txtPlyerName.Text = this.playername;
                this.avatarpath = streamReader.ReadLine();
            }

            Image backgroundImage;
            try
            {
                if (!File.Exists(this.avatarpath))
                {
                    throw new FileNotFoundException();
                }

                backgroundImage = Image.FromFile(this.avatarpath);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Avatar không tồn tại. Sẽ thay thế bằng avatar mặc định!", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.avatarpath = ".\\Emo\\Avatar\\9.jpg";
                backgroundImage = Image.FromFile(this.avatarpath);

                // Ghi lại avatar mặc định vào file config
                using (StreamWriter writer = new StreamWriter(configPath))
                {
                    writer.WriteLine(this.playername);
                    writer.WriteLine(this.avatarpath);
                }
            }

            this.pbxAvatar.BackgroundImage = backgroundImage;
            base.BeginInvoke(new MethodInvoker(this.focusTxtBox));

        }

        private void cmdCancel_Click_1(object sender, EventArgs e)
        {
            base.Close();
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            // Thiết lập OpenFileDialog cho việc chọn ảnh
            this.odlAvatar.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            this.odlAvatar.Title = "Chọn Avatar";

            if (this.odlAvatar.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn ảnh đã chọn
                this.avatarpath = this.odlAvatar.FileName;

                // Cập nhật ảnh trên PictureBox
                Image backgroundImage = Image.FromFile(this.avatarpath);
                this.pbxAvatar.BackgroundImage = backgroundImage;
            }
        }


        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtPlyerName.Text))
            {
                this.playername = this.txtPlyerName.Text;
            }

            // Ghi lại tên người chơi và avatar vào file config của Form1 này
            using (StreamWriter streamWriter = new StreamWriter(configPath))
            {
                streamWriter.WriteLine(this.playername);
                streamWriter.WriteLine(this.avatarpath);
            }


            base.Close();
        }


        private void pbxAvatar_Click(object sender, EventArgs e)
        {

        }
    }
}