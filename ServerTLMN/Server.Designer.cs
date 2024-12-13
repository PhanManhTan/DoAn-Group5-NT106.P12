namespace ServerTLMN
{
    partial class Server
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server));
            this.buttonGetIP = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.UserCount = new System.Windows.Forms.TextBox();
            this.RoomCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.buttonListen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonGetIP
            // 
            this.buttonGetIP.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonGetIP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonGetIP.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 10F);
            this.buttonGetIP.ForeColor = System.Drawing.Color.White;
            this.buttonGetIP.Location = new System.Drawing.Point(13, 183);
            this.buttonGetIP.Name = "buttonGetIP";
            this.buttonGetIP.Size = new System.Drawing.Size(238, 46);
            this.buttonGetIP.TabIndex = 21;
            this.buttonGetIP.Text = "Get server\'s IP address";
            this.buttonGetIP.UseVisualStyleBackColor = false;
            this.buttonGetIP.Click += new System.EventHandler(this.buttonGetIP_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 10F);
            this.label3.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label3.Location = new System.Drawing.Point(23, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 28);
            this.label3.TabIndex = 20;
            // 
            // textBoxIP
            // 
            this.textBoxIP.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxIP.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxIP.ForeColor = System.Drawing.Color.Gray;
            this.textBoxIP.Location = new System.Drawing.Point(41, 257);
            this.textBoxIP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.ReadOnly = true;
            this.textBoxIP.Size = new System.Drawing.Size(178, 34);
            this.textBoxIP.TabIndex = 19;
            this.textBoxIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserCount
            // 
            this.UserCount.BackColor = System.Drawing.SystemColors.Window;
            this.UserCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserCount.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserCount.ForeColor = System.Drawing.Color.Gray;
            this.UserCount.Location = new System.Drawing.Point(203, 385);
            this.UserCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UserCount.Name = "UserCount";
            this.UserCount.ReadOnly = true;
            this.UserCount.Size = new System.Drawing.Size(39, 34);
            this.UserCount.TabIndex = 18;
            this.UserCount.Text = "0";
            this.UserCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RoomCount
            // 
            this.RoomCount.BackColor = System.Drawing.SystemColors.Window;
            this.RoomCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RoomCount.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoomCount.ForeColor = System.Drawing.Color.Gray;
            this.RoomCount.Location = new System.Drawing.Point(203, 336);
            this.RoomCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RoomCount.Name = "RoomCount";
            this.RoomCount.ReadOnly = true;
            this.RoomCount.Size = new System.Drawing.Size(39, 34);
            this.RoomCount.TabIndex = 17;
            this.RoomCount.Text = "0";
            this.RoomCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 10F);
            this.label2.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label2.Location = new System.Drawing.Point(13, 385);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 28);
            this.label2.TabIndex = 16;
            this.label2.Text = "Existing users";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 10F);
            this.label1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label1.Location = new System.Drawing.Point(13, 338);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 28);
            this.label1.TabIndex = 15;
            this.label1.Text = "Available rooms";
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.ForeColor = System.Drawing.Color.Gray;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(266, 16);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(541, 409);
            this.listView1.TabIndex = 14;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // buttonListen
            // 
            this.buttonListen.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonListen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonListen.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 10F);
            this.buttonListen.ForeColor = System.Drawing.Color.White;
            this.buttonListen.Location = new System.Drawing.Point(59, 82);
            this.buttonListen.Name = "buttonListen";
            this.buttonListen.Size = new System.Drawing.Size(136, 58);
            this.buttonListen.TabIndex = 13;
            this.buttonListen.Text = "LISTEN";
            this.buttonListen.UseVisualStyleBackColor = false;
            this.buttonListen.Click += new System.EventHandler(this.buttonListen_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 440);
            this.Controls.Add(this.buttonGetIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.UserCount);
            this.Controls.Add(this.RoomCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.buttonListen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Server";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Server_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGetIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox UserCount;
        private System.Windows.Forms.TextBox RoomCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button buttonListen;
    }
}

