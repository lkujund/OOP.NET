using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupDAL;

namespace WorldCup_WinForms.Panels
{
    public class PlayerRankControl : Panel, IComparable<PlayerRankControl>
    {
        public PictureBox pbPlayerImage;
        public Label lbName;
        public Label lbGoalNumber;
        public Label lbYellowCards;

        public PlayerRankControl()
        {
            InitializeComponents();
        }

        public int CompareTo(PlayerRankControl? other)
        {
            return -int.Parse(lbGoalNumber.Text.Split(" ")[1]).CompareTo(int.Parse(other.lbGoalNumber.Text.Split(" ")[1]));
        }


        private void InitializeComponents()
        {
            this.Width = 300;
            this.Height = 90;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.DarkSeaGreen;

            //PictureBox
            pbPlayerImage = new PictureBox();
            pbPlayerImage.Width = 60;
            pbPlayerImage.Height = 60;
            pbPlayerImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPlayerImage.Image = Image.FromFile(Repository.DEFAULT_IMAGE);
            pbPlayerImage.Location = new Point(10, 15);
            this.Controls.Add(pbPlayerImage);

            //Label name
            lbName = new Label();
            lbName.Text = "";
            lbName.Location = new Point(80, 15);
            lbName.AutoSize = true;
            this.Controls.Add(lbName);

            //Label goal number
            lbGoalNumber = new Label();
            lbGoalNumber.Text = "";
            lbGoalNumber.Location = new Point(80, 30);
            lbGoalNumber.AutoSize = true;
            this.Controls.Add(lbGoalNumber);

            //Label yellow cards
            lbYellowCards = new Label();
            lbYellowCards.Text = "";
            lbYellowCards.Location = new Point(80, 45);
            lbYellowCards.AutoSize = true;
            this.Controls.Add(lbYellowCards);


        }


    }
}
