namespace TienLenDoAn
{
    partial class SinglePlayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrPlayer = new System.Windows.Forms.Timer(this.components);
            this.tmrCompDelay = new System.Windows.Forms.Timer(this.components);
            this.pbxClock = new System.Windows.Forms.PictureBox();
            this.pbrRemainTime = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pbxOpponent = new System.Windows.Forms.PictureBox();
            this.cmdPlay = new TienLenDoAn.CustomButton();
            this.cmdUnChose = new TienLenDoAn.CustomButton();
            this.cmdSkip = new TienLenDoAn.CustomButton();
            this.cmdDeal = new TienLenDoAn.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbxClock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOpponent)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrPlayer
            // 
            this.tmrPlayer.Interval = 155;
            this.tmrPlayer.Tick += new System.EventHandler(this.tmrPlayer_Tick_1);
            // 
            // tmrCompDelay
            // 
            this.tmrCompDelay.Interval = 500;
            this.tmrCompDelay.Tick += new System.EventHandler(this.tmrCompDelay_Tick_1);
            // 
            // pbxClock
            // 
            this.pbxClock.BackColor = System.Drawing.Color.Transparent;
            this.pbxClock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxClock.Image = global::TienLenDoAn.Properties.Resources.Clock_cooldown;
            this.pbxClock.Location = new System.Drawing.Point(178, 432);
            this.pbxClock.Margin = new System.Windows.Forms.Padding(4);
            this.pbxClock.Name = "pbxClock";
            this.pbxClock.Size = new System.Drawing.Size(52, 53);
            this.pbxClock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxClock.TabIndex = 15;
            this.pbxClock.TabStop = false;
            this.pbxClock.Visible = false;
            this.pbxClock.Click += new System.EventHandler(this.pbxClock_Click);
            // 
            // pbrRemainTime
            // 
            this.pbrRemainTime.Location = new System.Drawing.Point(31, 449);
            this.pbrRemainTime.Margin = new System.Windows.Forms.Padding(4);
            this.pbrRemainTime.Name = "pbrRemainTime";
            this.pbrRemainTime.Size = new System.Drawing.Size(149, 18);
            this.pbrRemainTime.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbrRemainTime.TabIndex = 14;
            this.pbrRemainTime.Value = 100;
            this.pbrRemainTime.Visible = false;
            this.pbrRemainTime.Click += new System.EventHandler(this.pbrRemainTime_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.AliceBlue;
            this.lblStatus.Location = new System.Drawing.Point(13, 627);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(222, 29);
            this.lblStatus.TabIndex = 20;
            this.lblStatus.Text = "Please click Deal!";
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // pbxOpponent
            // 
            this.pbxOpponent.Image = global::TienLenDoAn.Properties.Resources.pngegg;
            this.pbxOpponent.Location = new System.Drawing.Point(465, 12);
            this.pbxOpponent.Name = "pbxOpponent";
            this.pbxOpponent.Size = new System.Drawing.Size(144, 193);
            this.pbxOpponent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxOpponent.TabIndex = 21;
            this.pbxOpponent.TabStop = false;
            this.pbxOpponent.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // cmdPlay
            // 
            this.cmdPlay.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.cmdPlay.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.cmdPlay.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.cmdPlay.BorderRadius = 40;
            this.cmdPlay.BorderSize = 0;
            this.cmdPlay.FlatAppearance.BorderSize = 0;
            this.cmdPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdPlay.ForeColor = System.Drawing.Color.White;
            this.cmdPlay.Location = new System.Drawing.Point(31, 575);
            this.cmdPlay.Name = "cmdPlay";
            this.cmdPlay.Size = new System.Drawing.Size(150, 40);
            this.cmdPlay.TabIndex = 19;
            this.cmdPlay.Text = "Đánh";
            this.cmdPlay.TextColor = System.Drawing.Color.White;
            this.cmdPlay.UseVisualStyleBackColor = true;
            this.cmdPlay.Visible = false;
            this.cmdPlay.Click += new System.EventHandler(this.cmdPlay_Click);
            // 
            // cmdUnChose
            // 
            this.cmdUnChose.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.cmdUnChose.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.cmdUnChose.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.cmdUnChose.BorderRadius = 40;
            this.cmdUnChose.BorderSize = 0;
            this.cmdUnChose.FlatAppearance.BorderSize = 0;
            this.cmdUnChose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdUnChose.ForeColor = System.Drawing.Color.White;
            this.cmdUnChose.Location = new System.Drawing.Point(31, 529);
            this.cmdUnChose.Name = "cmdUnChose";
            this.cmdUnChose.Size = new System.Drawing.Size(150, 40);
            this.cmdUnChose.TabIndex = 18;
            this.cmdUnChose.Text = "Bỏ chọn tất cả";
            this.cmdUnChose.TextColor = System.Drawing.Color.White;
            this.cmdUnChose.UseVisualStyleBackColor = false;
            this.cmdUnChose.Visible = false;
            this.cmdUnChose.Click += new System.EventHandler(this.cmdUnChose_Click);
            // 
            // cmdSkip
            // 
            this.cmdSkip.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.cmdSkip.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.cmdSkip.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.cmdSkip.BorderRadius = 40;
            this.cmdSkip.BorderSize = 0;
            this.cmdSkip.Enabled = false;
            this.cmdSkip.FlatAppearance.BorderSize = 0;
            this.cmdSkip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSkip.ForeColor = System.Drawing.Color.White;
            this.cmdSkip.Location = new System.Drawing.Point(31, 483);
            this.cmdSkip.Name = "cmdSkip";
            this.cmdSkip.Size = new System.Drawing.Size(150, 40);
            this.cmdSkip.TabIndex = 17;
            this.cmdSkip.Text = "Bỏ qua";
            this.cmdSkip.TextColor = System.Drawing.Color.White;
            this.cmdSkip.UseVisualStyleBackColor = false;
            this.cmdSkip.Visible = false;
            this.cmdSkip.Click += new System.EventHandler(this.cmdSkip_Click);
            // 
            // cmdDeal
            // 
            this.cmdDeal.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.cmdDeal.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.cmdDeal.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.cmdDeal.BorderRadius = 40;
            this.cmdDeal.BorderSize = 0;
            this.cmdDeal.FlatAppearance.BorderSize = 0;
            this.cmdDeal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDeal.ForeColor = System.Drawing.Color.White;
            this.cmdDeal.Location = new System.Drawing.Point(912, 612);
            this.cmdDeal.Name = "cmdDeal";
            this.cmdDeal.Size = new System.Drawing.Size(150, 40);
            this.cmdDeal.TabIndex = 16;
            this.cmdDeal.Text = "Deal";
            this.cmdDeal.TextColor = System.Drawing.Color.White;
            this.cmdDeal.UseVisualStyleBackColor = false;
            this.cmdDeal.Click += new System.EventHandler(this.cmdDeal_Click);
            // 
            // SinglePlayer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackgroundImage = global::TienLenDoAn.Properties.Resources.Background_Ingame1;
            this.ClientSize = new System.Drawing.Size(1074, 661);
            this.Controls.Add(this.pbxOpponent);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmdPlay);
            this.Controls.Add(this.cmdUnChose);
            this.Controls.Add(this.cmdSkip);
            this.Controls.Add(this.cmdDeal);
            this.Controls.Add(this.pbxClock);
            this.Controls.Add(this.pbrRemainTime);
            this.Name = "SinglePlayer";
            this.Text = "SinglePlayer";
            this.Load += new System.EventHandler(this.SinglePlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxClock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOpponent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmrPlayer;
        private System.Windows.Forms.Timer tmrCompDelay;
        private System.Windows.Forms.PictureBox pbxClock;
        private System.Windows.Forms.ProgressBar pbrRemainTime;
        private CustomButton cmdDeal;
        private CustomButton cmdSkip;
        private CustomButton cmdUnChose;
        private CustomButton cmdPlay;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox pbxOpponent;
    }
}