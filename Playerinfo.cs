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
    public partial class Playerinfo : Form
    {
        public Playerinfo(bool isPlayer)
        {
            InitializeComponent();
            base.Opacity = 0.0;
            this.isPlayer = isPlayer;
        }


        public void BringFront()
        {
            base.BringToFront();
        }
        public void Win()
        {
            Random random = new Random();
            string str = random.Next(1, 23).ToString();
            this.pbxAvatar.BackgroundImage = Image.FromFile(".\\Emo\\Winner\\" + str + ".jpg");
        }

        public void Lose()
        {
            Random random = new Random();
            string str = random.Next(1, 33).ToString();
            this.pbxAvatar.BackgroundImage = Image.FromFile(".\\Emo\\Loser\\" + str + ".jpg");
        }
        public void ReturnAvatar()
        {
            this.pbxAvatar.BackgroundImage = Image.FromFile(this.avatarpath);
        }

        public int x;

        private bool isPlayer = true;

        public string avatarpath;

        public string playerName;

        private void Playerinfo_Load(object sender, EventArgs e)
        {
            this.x = base.Location.X;
            if (this.isPlayer)
            {
                StreamReader streamReader = new StreamReader(".\\Config.config");
                FileInfo fileInfo = new FileInfo(".\\Config.config");
                this.playerName = streamReader.ReadLine();
                this.lblPlayerName.Text = this.playerName;
                this.avatarpath = streamReader.ReadLine();
                streamReader.Close();
                Image backgroundImage;
                try
                {
                    backgroundImage = Image.FromFile(this.avatarpath);
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Your avatar is not found. Default avatar will be replaced instead!", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.avatarpath = ".\\Emo\\Avatar\\8.jpg";
                    backgroundImage = Image.FromFile(this.avatarpath);
                    StreamWriter streamWriter = new StreamWriter(".\\Config.config");
                    streamWriter.Write(this.lblPlayerName.Text + "\r\n");
                    streamWriter.Write(this.avatarpath);
                    streamWriter.Close();
                }
                this.pbxAvatar.BackgroundImage = backgroundImage;
                streamReader.Close();
                this.tmrRight.Start();
            }
            else
            {
                Random random = new Random();
                string str = random.Next(1, 70).ToString();
                this.avatarpath = ".\\Emo\\Avatar\\" + str + ".jpg";
                this.pbxAvatar.BackgroundImage = Image.FromFile(this.avatarpath);
                this.tmrLeft.Start();
            }
        }




        private void lblPlayerName_Click(object sender, EventArgs e)
        {

        }

        private void pbxAvatar_Click(object sender, EventArgs e)
        {

        }

        private void tmrRight_Tick_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tmrLeft_Tick_1(object sender, EventArgs e)
        {

        }

        private void tmrDesOpacity_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pbxAvatar_Click_1(object sender, EventArgs e)
        {

        }


        private void lblPlayerName_Click_1(object sender, EventArgs e)
        {

        }

        private void tmrRight_Tick(object sender, EventArgs e)
        {
            if (base.Location.X + 5 < this.x + base.Width)
            {
                base.Location = new Point(base.Location.X + 5, base.Location.Y);
                base.Opacity += 0.02;
            }
            else
            {
                this.tmrRight.Stop();
                base.Location = new Point(this.x + base.Width + 5, base.Location.Y);
                base.Opacity = 1.0;
            }
        }

        private void tmrLeft_Tick(object sender, EventArgs e)
        {
            if (base.Location.X - 5 > this.x - base.Width)
            {
                base.Location = new Point(base.Location.X - 5, base.Location.Y);
                base.Opacity += 0.02;
            }
            else
            {
                this.tmrLeft.Stop();
                base.Location = new Point(this.x - base.Width - 5, base.Location.Y);
                base.Opacity = 1.0;
            }
        }

        private void tmrDesOpacity_Tick_2(object sender, EventArgs e)
        {
            if (base.Opacity > 0.0)
            {
                base.Opacity -= 0.05;
            }
            else
            {
                base.Close();
                this.tmrDesOpacity.Stop();
            }
        }

        private void lblPlayerName_Click_2(object sender, EventArgs e)
        {

        }
    }
}
