namespace TienLenDoAn
{
    partial class CreateOrJoin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateOrJoin));
            this.tbRoomID = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbRoomID = new System.Windows.Forms.Label();
            this.tbServerIP = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbRoomID
            // 
            this.tbRoomID.Location = new System.Drawing.Point(228, 158);
            this.tbRoomID.Name = "tbRoomID";
            this.tbRoomID.Size = new System.Drawing.Size(213, 26);
            this.tbRoomID.TabIndex = 6;
            this.tbRoomID.Visible = false;
            this.tbRoomID.TextChanged += new System.EventHandler(this.tbRoomID_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(65, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 77);
            this.button1.TabIndex = 9;
            this.button1.Text = "Create New";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(287, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 77);
            this.button2.TabIndex = 10;
            this.button2.Text = "Join Room";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(174, 293);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(154, 77);
            this.button3.TabIndex = 11;
            this.button3.Text = "Start";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Server\'s IP";
            // 
            // lbRoomID
            // 
            this.lbRoomID.AutoSize = true;
            this.lbRoomID.Location = new System.Drawing.Point(78, 164);
            this.lbRoomID.Name = "lbRoomID";
            this.lbRoomID.Size = new System.Drawing.Size(73, 20);
            this.lbRoomID.TabIndex = 13;
            this.lbRoomID.Text = "Room ID";
            this.lbRoomID.Visible = false;
            // 
            // tbServerIP
            // 
            this.tbServerIP.Location = new System.Drawing.Point(227, 220);
            this.tbServerIP.Name = "tbServerIP";
            this.tbServerIP.Size = new System.Drawing.Size(214, 26);
            this.tbServerIP.TabIndex = 14;
            this.tbServerIP.Text = "127.0.0.1";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(506, 411);
            this.Controls.Add(this.tbServerIP);
            this.Controls.Add(this.lbRoomID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbRoomID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form3";
            this.Text = "Join or Create Room";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbRoomID;
        private System.Windows.Forms.TextBox tbServerIP1;
        private System.Windows.Forms.TextBox tbServerIP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbRoomID;
     //   private System.Windows.Forms.TextBox tbServerIP1;
    }
}