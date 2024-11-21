using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TienLenDoAn.Properties;

namespace TienLenDoAn
{
    public partial class SinglePlayer : Form
    {
        public SinglePlayer(Form frm)
        {
            InitializeComponent();
            this.frmOpen = frm;
            this.LocationChanged += new EventHandler(Singleplayer_LocationChanged);
            this.FormClosing += new FormClosingEventHandler(Singleplayer_FormClosing);
        }

        private void SinglePlayer_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }   

        private void card1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cmdDeal_Click(object sender, EventArgs e)
        {
            if (this.isFirstRound)
            {
                // Lấy tọa độ trung tâm của form Singleplayer
                int centerX = this.Location.X + (this.Width / 2);
                int centerY = this.Location.Y + (this.Height / 2);

                // Playerinfo của người chơi - nằm ở right center của Singleplayer form
                if (this.frmPlyInfo == null)
                {
                    this.frmPlyInfo = new Playerinfo(true);
                    this.frmPlyInfo.StartPosition = FormStartPosition.Manual; // Đặt vị trí thủ công
                                                                              // Đặt ở cạnh bên phải, trung tâm theo chiều dọc
                    this.frmPlyInfo.Location = new Point(this.Location.X + this.Width * 4 / 5 - 10, centerY - (this.frmPlyInfo.Height / 2));
                    this.frmPlyInfo.Show();
                }

                // Playerinfo của máy tính - nằm ở left center của Singleplayer form
                if (this.frmCompInfo == null)
                {
                    this.frmCompInfo = new Playerinfo(false);
                    this.frmCompInfo.StartPosition = FormStartPosition.Manual; // Đặt vị trí thủ công
                                                                               // Đặt ở cạnh bên trái, trung tâm theo chiều dọc
                    this.frmCompInfo.Location = new Point(this.Location.X + 13, centerY - (this.frmCompInfo.Height / 2));
                    this.frmCompInfo.lblPlayerName.Text = "Computer";
                    this.frmCompInfo.Show();
                }

                this.isFirstRound = false;
            }
            this.frmPlyInfo.ReturnAvatar();
            this.frmCompInfo.ReturnAvatar();
            base.BringToFront();
            this.RemovePlayedCard();
            this.RemovePlayerCards();
            this._Player.PlayerCard.Clear();
            this._Computer.PlayerCard.Clear();
            this.PreMoveCards.Clear();
            this.Deal();
            if (this.CheckSmallestCard())
            {
                this.PlayerBeginTurn();
            }
            else
            {
                this.ComputerBeginTurn();
            }
            this.cmdDeal.Visible = false;
            this.pbxOpponent.Visible = true;
            this.cmdPlay.Visible = true;
            this.cmdSkip.Visible = true;
            this.cmdUnChose.Visible = true;
        }

        private void Singleplayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.frmPlyInfo != null)
            {
                this.frmPlyInfo.Close();
            }

            if (this.frmCompInfo != null)
            {
                this.frmCompInfo.Close();
            }
        }

        private void Singleplayer_LocationChanged(object sender, EventArgs e)
        {
            // Lấy tọa độ trung tâm của form Singleplayer
            int centerX = this.Location.X + (this.Width / 2);
            int centerY = this.Location.Y + (this.Height / 2);

            // Cập nhật vị trí của frmPlyInfo (Playerinfo người chơi)
            if (this.frmPlyInfo != null && this.frmPlyInfo.Created)
            {
                this.frmPlyInfo.Invoke(new SinglePlayer.Relocate(this.Rec), new object[]
                {
                this.frmPlyInfo,
                this.Location.X + this.Width - 7,   // Cạnh phải form Singleplayer, căn giữa theo chiều dọc
                centerY - (this.frmPlyInfo.Height / 2)        // Căn giữa theo chiều dọc
                });
            }

            // Cập nhật vị trí của frmCompInfo (Playerinfo máy tính)
            if (this.frmCompInfo != null && this.frmCompInfo.Created)
            {
                this.frmCompInfo.Invoke(new SinglePlayer.Relocate(this.Rec), new object[]
                {
                this.frmCompInfo,
                this.Location.X - frmCompInfo.Width + 7,        // Cạnh trái form Singleplayer, căn giữa theo chiều dọc
                centerY - (this.frmCompInfo.Height / 2)        // Căn giữa theo chiều dọc
                });
            }
        }

        public void Choseobj_Click(object sender, EventArgs e)
        {
            Card card = sender as Card;
            if (card.Choose)
            {
                this._Player.ChoseCard.Remove(card);
            }
            else
            {
                this._Player.ChoseCard.Add(card);
            }
            if (this.PlayerOnTurn)
            {
                if (this._Player.DoCheck(this.PreMoveCards))
                {
                    this.cmdPlay.Enabled = true;
                }
                else
                {
                    this.cmdPlay.Enabled = false;
                }
            }
            if (this._Player.ChoseCard.Count != 0)
            {
                this.cmdUnChose.Enabled = true;
            }
            else
            {
                this.cmdUnChose.Enabled = false;
            }
            card.Toggle();
        }

        private void cmdPlay_Click(object sender, EventArgs e)
        {
            this.Played = true;
            this.PreMoveCards.Clear();
            this.PreMoveCards.AddRange(this._Player.ChoseCard);
            for (int i = 0; i < this._Player.ChoseCard.Count; i++)
            {
                this._Player.PlayerCard.Remove(this._Player.ChoseCard[i]);
            }
            this._Player.ChoseCard.Clear();
            this.DrawPlayedCard();
            this.DrawPlayerCard(ref this._Player.PlayerCard);
            this.cmdPlay.Enabled = false;
            this.cmdUnChose.Enabled = false;
            this.cmdSkip.Enabled = false;
            if (this._Player.PlayerCard.Count == 0)
            {
                this.pbxOpponent.Visible = false;
                this.cmdPlay.Visible = false;
                this.cmdSkip.Visible = false;
                this.cmdUnChose.Visible = false;
                this.cmdDeal.Visible = true;
                this.DrawComputerCard(ref this._Computer.PlayerCard);
                this.UnClickableCards(this._Computer.PlayerCard);
                this.tmrPlayer.Stop();
                this.pbrRemainTime.Visible = false;
                this.pbxClock.Visible = false;
                this.lblStatus.Text = "You Win!";
                this.frmPlyInfo.Win();
                this.frmCompInfo.Lose();
                this.Wintimes++;
                this.frmPlyInfo.lblWinNum.Text = this.Wintimes.ToString();
                this.frmCompInfo.lblLoseNum.Text = this.Wintimes.ToString();
            }
            else
            {
                this.remaintime = 1;
            }
        }

        private void pbrRemainTime_Click(object sender, EventArgs e)
        {

        }

        private void RemovePlayedCard()
        {
            if (this.PlayedCard.Count != 0)
            {
                for (int i = 0; i < this.PlayedCard.Count<Card>(); i++)
                {
                    base.Controls.Remove(this.PlayedCard[i]);
                }
            }
            this.PlayedCard.Clear();
        }

        private void RemovePlayerCards()
        {
            if (this._Computer.PlayerCard.Count != 0)
            {
                for (int i = 0; i < this._Computer.PlayerCard.Count; i++)
                {
                    base.Controls.Remove(this._Computer.PlayerCard[i]);
                }
            }
            if (this._Player.PlayerCard.Count != 0)
            {
                for (int i = 0; i < this._Player.PlayerCard.Count; i++)
                {
                    base.Controls.Remove(this._Player.PlayerCard[i]);
                }
            }
        }
        private void UnChoseAll()
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
        private void UnClickableCards(List<Card> List)
        {
            for (int i = 0; i < List.Count; i++)
            {
                List[i].Click -= this.Choseobj_Click;
            }
        }

        private void DrawComputerCard(ref List<Card> CardList)
        {
            int num = CardList.Count<Card>();
            int num2 = base.Width - (base.Width - (22 * CardList.Count<Card>() + 112)) / 2 - 112;
            for (int i = num - 1; i >= 0; i--)
            {
                Card card = CardList[i];
                card.Location = new Point(num2, 20);
                card.Visible = true;
                base.Controls.Add(card);
                num2 -= 22;
                num = CardList.Count<Card>();
            }
        }

        private void DrawPlayerCard(ref List<Card> CardList)
        {
            int num = CardList.Count<Card>();
            int num2 = base.Width - (base.Width - (22 * CardList.Count<Card>() + 112)) / 2 - 112;
            for (int i = num - 1; i >= 0; i--)
            {
                Card card = CardList[i];
                card.Location = new Point(num2, 414);
                base.Controls.Add(card);
                num2 -= 22;
                num = CardList.Count<Card>();
            }
        }
        private void DrawPlayedCard()
        {
            this.RemovePlayedCard();
            int num = this.PreMoveCards.Count<Card>();
            int num2 = base.Width - (base.Width - (22 * this.PreMoveCards.Count<Card>() + 112)) / 2 - 112;
            for (int i = num - 1; i >= 0; i--)
            {
                Card card = this.PreMoveCards[i];
                this.PlayedCard.Add(card);
                card.Location = new Point(num2, 232);
                card.Visible = true;
                num2 -= 22;
                num = this.PreMoveCards.Count<Card>();
            }
            this.UnClickableCards(this.PlayedCard);
        }

        public void Deal()
        {
            List<NameofCard> list = new List<NameofCard>();
            for (int i = 3; i < 16; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    list.Add((NameofCard)i);
                }
            }
            List<TypeofCard> list2 = new List<TypeofCard>();
            for (int j = 1; j < 14; j++)
            {
                for (int k = 1; k < 5; k++)
                {
                    list2.Add((TypeofCard)k);
                }
            }
            List<Card> list3 = new List<Card>();
            for (int l = 0; l < 52; l++)
            {
                Card item = new Card(list[l], list2[l]);
                list3.Add(item);
            }
            int num = 52;
            Random random = new Random();
            for (int m = 13; m > 0; m--)
            {
                Card card = list3[random.Next(0, num)];
                card.Click += this.Choseobj_Click;
                this._Player.PlayerCard.Add(card);
                list3.Remove(card);
                this._Player.SortCList(ref this._Player.PlayerCard);
                num--;
            }
            for (int m = 13; m > 0; m--)
            {
                Card card = list3[random.Next(0, num)];
                card.Click += this.Choseobj_Click;
                this._Computer.PlayerCard.Add(card);
                list3.Remove(card);
                this._Computer.SortCList(ref this._Computer.PlayerCard);
                num--;
            }
            this.DrawPlayerCard(ref this._Player.PlayerCard);
            int num2 = this._Computer.PlayerCard.Count<Card>();
            for (int m = num2 - 1; m >= 0; m--)
            {
                Card card = this._Computer.PlayerCard[m];
                card.Visible = false;
                card.Location = new Point(0, 0);
                base.Controls.Add(card);
                num2 = this._Computer.PlayerCard.Count<Card>();
            }
        }

        private bool CheckSmallestCard()
        {
            bool result;
            switch (this._Player.CheckSingle(this._Player.PlayerCard[0], this._Computer.PlayerCard[0]))
            {
                case false:
                    result = true;
                    break;
                case true:
                    result = false;
                    break;
                default:
                    result = true;
                    break;
            }
            return result;
        }

        private void PlayerBeginTurn()
        {
            this.pbrRemainTime.Visible = true;
            this.pbxClock.Visible = true;
            this.tmrPlayer.Start();
            this.PlayerOnTurn = true;
            this.lblStatus.Text = "Your turn!";
            if (this._Player.DoCheck(this.PreMoveCards))
            {
                this.cmdPlay.Enabled = true;
            }
            if (this.PreMoveCards.Count != 0)
            {
                this.cmdSkip.Enabled = true;
            }
        }

        private void ComputerBeginTurn()
        {
            this.tmrPlayer.Stop();
            this.pbrRemainTime.Visible = false;
            this.pbxClock.Visible = false;
            this.PlayerOnTurn = false;
            this.Played = false;
            this.lblStatus.Text = "Computer is thinking!";
            Random random = new Random();
            this.compdelay = random.Next(2, 5);
            this.tmrCompDelay.Start();
        }

        private void ComputerPlay()
        {
            if (this.PreMoveCards.Count == 0)
            {
                this._Computer.ChoseCard.Add(this._Computer.PlayerCard[0]);
            }
            else
            {
                this._Computer.FindValidCardtoPlay(this.PreMoveCards);
            }
            if (this._Computer.ChoseCard.Count == 0)
            {
                this.RemovePlayedCard();
                this.PreMoveCards.Clear();
                this.remaintime = 100;
                this.PlayerBeginTurn();
            }
            else
            {
                this.PreMoveCards.Clear();
                this.PreMoveCards.AddRange(this._Computer.ChoseCard);
                for (int i = 0; i < this._Computer.ChoseCard.Count; i++)
                {
                    this._Computer.PlayerCard.Remove(this._Computer.ChoseCard[i]);
                }
                this._Computer.ChoseCard.Clear();
                this.DrawPlayedCard();
                if (this._Computer.PlayerCard.Count == 0)
                {
                    this.UnChoseAll();
                    this.LoseTimes++;
                    this.frmPlyInfo.lblLoseNum.Text = this.LoseTimes.ToString();
                    this.frmCompInfo.lblWinNum.Text = this.LoseTimes.ToString();
                    this.lblStatus.Text = "You lose!";
                    this.frmCompInfo.Win();
                    this.frmPlyInfo.Lose();
                    this.UnClickableCards(this._Player.PlayerCard);
                    this.pbxOpponent.Visible = false;
                    this.cmdPlay.Visible = false;
                    this.cmdSkip.Visible = false;
                    this.cmdUnChose.Visible = false;
                    this.cmdDeal.Visible = true;
                }
                else
                {
                    this.remaintime = 100;
                    this.PlayerBeginTurn();
                }
            }
        }

        private void tmrPlayer_Tick(object sender, EventArgs e)
        {
            this.pbrRemainTime.Value = this.remaintime;
            if (this.remaintime <= 20)
            {
                if (this.pbxClock.Visible)
                {
                    this.pbxClock.Visible = false;
                }
                else
                {
                    this.pbxClock.Visible = true;
                }
            }
            if (this.remaintime == 0)
            {
                if (!this.Played)
                {
                    this.remaintime = 100;
                    this.RemovePlayedCard();
                    this.PreMoveCards.Clear();
                    this.ComputerBeginTurn();
                }
                else
                {
                    this.remaintime = 100;
                    this.ComputerBeginTurn();
                }
            }
            else
            {
                this.remaintime--;
            }
        }




        public Player _Player = new Player();

        public Player _Computer = new Player();

        public List<Card> PreMoveCards = new List<Card>();

        public List<Card> PlayedCard = new List<Card>();

        private int remaintime = 100;

        private int compdelay;

        public bool PlayerOnTurn = false;

        public bool Played = false;

        public Form frmOpen;

        public Playerinfo frmPlyInfo;

        public Playerinfo frmCompInfo;

        public bool right;

        public bool down = false;

        public bool isFirstRound = true;

        public int Wintimes;

        public int LoseTimes = 0;

        private void tmrCompDelay_Tick_1(object sender, EventArgs e)
        {
            if (this.compdelay == 0)
            {
                this.tmrCompDelay.Stop();
                this.ComputerPlay();
            }
            this.compdelay--;
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void pbxClock_Click(object sender, EventArgs e)
        {

        }

        private void cmdUnChose_Click(object sender, EventArgs e)
        {
            this.UnChoseAll();
        }

        private void cmdSkip_Click(object sender, EventArgs e)
        {
            this.UnChoseAll();
            this.PreMoveCards.Clear();
            this.RemovePlayedCard();
            this.cmdPlay.Enabled = false;
            this.cmdUnChose.Enabled = false;
            this.cmdSkip.Enabled = false;
            this.remaintime = 1;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void tmrPlayer_Tick_1(object sender, EventArgs e)
        {
            this.pbrRemainTime.Value = this.remaintime;
            if (this.remaintime <= 20)
            {
                if (this.pbxClock.Visible)
                {
                    this.pbxClock.Visible = false;
                }
                else
                {
                    this.pbxClock.Visible = true;
                }
            }
            if (this.remaintime == 0)
            {
                if (!this.Played)
                {
                    this.remaintime = 100;
                    this.RemovePlayedCard();
                    this.PreMoveCards.Clear();
                    this.ComputerBeginTurn();
                }
                else
                {
                    this.remaintime = 100;
                    this.ComputerBeginTurn();
                }
            }
            else
            {
                this.remaintime--;
            }
        }

        private void Singlerplayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Kiểm tra nếu frmPlyInfo đã được tạo ra thì tắt
            if (this.frmPlyInfo != null && !this.frmPlyInfo.IsDisposed)
            {
                this.frmPlyInfo.Close();
            }

            // Kiểm tra nếu frmCompInfo đã được tạo ra thì tắt
            if (this.frmCompInfo != null && !this.frmCompInfo.IsDisposed)
            {
                this.frmCompInfo.Close();
            }
        }

        private void RecX(Form frm, int x)
        {
            frm.Location = new Point(x, frm.Location.Y);
        }

        private void RecY(Form frm, int y)
        {
            frm.Location = new Point(frm.Location.X, y);
        }

        private void Rec(Form frm, int x, int y)
        {
            frm.Location = new Point(x, y);
        }

        public delegate void RelocateX(Form frm, int x);

        public delegate void RelocateY(Form frm, int y);

        public delegate void Relocate(Form frm, int x, int y);
    }
}
