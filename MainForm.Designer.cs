namespace TienLenDoAn
{
    partial class TienLenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TienLenForm));
            this.exitbutton = new TienLenDoAn.CustomButton();
            this.OptionButton = new TienLenDoAn.CustomButton();
            this.MultiplayButton = new TienLenDoAn.CustomButton();
            this.SingleButton = new TienLenDoAn.CustomButton();
            this.SuspendLayout();
            // 
            // exitbutton
            // 
            this.exitbutton.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.exitbutton.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.exitbutton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.exitbutton.BorderRadius = 40;
            this.exitbutton.BorderSize = 0;
            this.exitbutton.FlatAppearance.BorderSize = 0;
            this.exitbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitbutton.ForeColor = System.Drawing.Color.White;
            this.exitbutton.Location = new System.Drawing.Point(445, 446);
            this.exitbutton.Name = "exitbutton";
            this.exitbutton.Size = new System.Drawing.Size(187, 61);
            this.exitbutton.TabIndex = 3;
            this.exitbutton.Text = "Exit";
            this.exitbutton.TextColor = System.Drawing.Color.White;
            this.exitbutton.UseVisualStyleBackColor = false;
            this.exitbutton.Click += new System.EventHandler(this.customButton4_Click);
            // 
            // OptionButton
            // 
            this.OptionButton.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.OptionButton.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.OptionButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.OptionButton.BorderRadius = 40;
            this.OptionButton.BorderSize = 0;
            this.OptionButton.FlatAppearance.BorderSize = 0;
            this.OptionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptionButton.ForeColor = System.Drawing.Color.White;
            this.OptionButton.Location = new System.Drawing.Point(445, 358);
            this.OptionButton.Name = "OptionButton";
            this.OptionButton.Size = new System.Drawing.Size(187, 61);
            this.OptionButton.TabIndex = 2;
            this.OptionButton.Text = "Option";
            this.OptionButton.TextColor = System.Drawing.Color.White;
            this.OptionButton.UseVisualStyleBackColor = false;
            this.OptionButton.Click += new System.EventHandler(this.customButton3_Click);
            // 
            // MultiplayButton
            // 
            this.MultiplayButton.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.MultiplayButton.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.MultiplayButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.MultiplayButton.BorderRadius = 40;
            this.MultiplayButton.BorderSize = 0;
            this.MultiplayButton.FlatAppearance.BorderSize = 0;
            this.MultiplayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MultiplayButton.ForeColor = System.Drawing.Color.White;
            this.MultiplayButton.Location = new System.Drawing.Point(445, 270);
            this.MultiplayButton.Name = "MultiplayButton";
            this.MultiplayButton.Size = new System.Drawing.Size(187, 61);
            this.MultiplayButton.TabIndex = 1;
            this.MultiplayButton.Text = "Multiplay";
            this.MultiplayButton.TextColor = System.Drawing.Color.White;
            this.MultiplayButton.UseVisualStyleBackColor = false;
            this.MultiplayButton.Click += new System.EventHandler(this.MultiplayButton_Click);
            // 
            // SingleButton
            // 
            this.SingleButton.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.SingleButton.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.SingleButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.SingleButton.BorderRadius = 40;
            this.SingleButton.BorderSize = 0;
            this.SingleButton.FlatAppearance.BorderSize = 0;
            this.SingleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SingleButton.ForeColor = System.Drawing.Color.Transparent;
            this.SingleButton.Location = new System.Drawing.Point(445, 183);
            this.SingleButton.Name = "SingleButton";
            this.SingleButton.Size = new System.Drawing.Size(187, 61);
            this.SingleButton.TabIndex = 0;
            this.SingleButton.Text = "Single Play";
            this.SingleButton.TextColor = System.Drawing.Color.Transparent;
            this.SingleButton.UseVisualStyleBackColor = false;
            this.SingleButton.Click += new System.EventHandler(this.customButton1_Click_1);
            // 
            // TienLenForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackgroundImage = global::TienLenDoAn.Properties.Resources.Backgrounddes;
            this.ClientSize = new System.Drawing.Size(1052, 586);
            this.Controls.Add(this.exitbutton);
            this.Controls.Add(this.OptionButton);
            this.Controls.Add(this.MultiplayButton);
            this.Controls.Add(this.SingleButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TienLenForm";
            this.Text = "Tiến Lên Miền Nam";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomButton SingleButton;
        private CustomButton MultiplayButton;
        private CustomButton OptionButton;
        private CustomButton exitbutton;
    }
}

