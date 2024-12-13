using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TienLenDoAn;

namespace TienLenDoAn
{
    public class Card : UserControl
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Card
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Card";
            this.Size = new System.Drawing.Size(149, 187); 
            this.Load += new System.EventHandler(this.Card_Load);
            this.ResumeLayout(false);

        }
        public void ChangeSize(int width, int height)
        {
            

        }
        public NameofCard NameOfCard
        {
            get
            {
                return this._nameOfCard;
            }
            set
            {
                this._nameOfCard = value;
            }
        }

        public TypeofCard TypeOfCard
        {
            get
            {
                return this._typeOfCard;
            }
            set
            {
                this._typeOfCard = value;
            }
        }

        public bool Choose
        {
            get
            {
                return this.choose;
            }
            set
            {
                this.choose = value;
            }
        }
        public Card()
        {
            this.InitializeComponent();
        }

        public Card(NameofCard nameOfCard, TypeofCard typeOfCard)
        {
            this.InitializeComponent();
            this._nameOfCard = nameOfCard;
            this._typeOfCard = typeOfCard;
            string str = string.Format("{0:D2}{1}.jpg", nameOfCard.GetHashCode(), typeOfCard.GetHashCode());
            Image backgroundImage = Image.FromFile("Resources\\" + str);
            this.BackgroundImage = backgroundImage;
        }

        public void Toggle()
        {
            if (this.Choose == true)
            {
                base.Location = new Point(base.Location.X, base.Location.Y + 25);
                this.Choose = false;
            }
            else
            {
                base.Location = new Point(base.Location.X, base.Location.Y - 25);
                this.Choose = true;
            }
        }

        private IContainer components = null;

        private NameofCard _nameOfCard;

        private TypeofCard _typeOfCard;

        private bool choose;

        private void Card_Load(object sender, EventArgs e)
        {

        }
    }
}

