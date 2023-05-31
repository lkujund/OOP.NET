using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorldCupDAL;

namespace WorldCup_WinForms.Panels
{
    internal class StadiumControl : Panel
    {
        public Label lbLocation;
        public Label lbAttendance;
        public Label lbHomeTeam;
        public Label lbAwayTeam;

        public StadiumControl()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Width = 282;
            this.Height = 90;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.DarkSeaGreen;



            //Label name
            lbLocation = new Label();
            lbLocation.Text = "";
            lbLocation.Location = new Point(10, 15);
            lbLocation.AutoSize = true;
            this.Controls.Add(lbLocation);

            //Label number
            lbAttendance = new Label();
            lbAttendance.Text = "";
            lbAttendance.Location = new Point(10, 35);
            lbAttendance.AutoSize = true;
            this.Controls.Add(lbAttendance);

            //Label position
            lbHomeTeam = new Label();
            lbHomeTeam.Text = "";
            lbHomeTeam.Location = new Point(10, 55);
            lbHomeTeam.AutoSize = true;
            this.Controls.Add(lbHomeTeam);

            //Label position
            lbAwayTeam = new Label();
            lbAwayTeam.Text = "";
            lbAwayTeam.Location = new Point(150, 55);
            lbAwayTeam.AutoSize = true;
            this.Controls.Add(lbAwayTeam);


        }
    }
}
