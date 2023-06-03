using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCup_WinForms.Forms;
using WorldCupDAL;

namespace WorldCup_WinForms.Panels
{
    public partial class PlayerControl : Panel, IEquatable<PlayerControl?>
    {
        public PictureBox pbPlayerImage;
        public Label lbName;
        public Label lbNumber;
        public Label lbPosition;
        public Label lbCaptain;
        public Button btnAddImage;
        public Label lbFavouriteStar;

        public static PlayerControl? playerStartedDnD;


        public PlayerControl()
        {
            InitializeComponents();
        }



        private void InitializeComponents()
        {
            this.Width = 300;
            this.Height = 90;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor= Color.DarkSeaGreen;

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

            //Label number
            lbNumber = new Label();
            lbNumber.Text = "";
            lbNumber.Location = new Point(80, 30);
            lbNumber.AutoSize = true;
            this.Controls.Add(lbNumber);

            //Label position
            lbPosition = new Label();
            lbPosition.Text = "";
            lbPosition.Location = new Point(80, 45);
            lbPosition.AutoSize = true;
            this.Controls.Add(lbPosition);

            //Label for captain status
            lbCaptain = new Label();
            lbCaptain.ForeColor = Color.Red;
            lbCaptain.Text = "Captain";
            lbCaptain.Visible = false;
            lbCaptain.Location = new Point(80, 60);
            lbCaptain.AutoSize = true;
            this.Controls.Add(lbCaptain);

            lbFavouriteStar = new Label();
            lbFavouriteStar.Text = "★";
            lbFavouriteStar.Font = new Font("Arial", 14, FontStyle.Bold);
            lbFavouriteStar.ForeColor = Color.Gold;
            lbFavouriteStar.AutoSize = true;
            lbFavouriteStar.Location = new Point(275, 0);
            lbFavouriteStar.Visible = false;
            this.Controls.Add(lbFavouriteStar);

            //Button for adding an image
            btnAddImage = new Button();
            btnAddImage.Image = Image.FromFile(Repository.IMG_ICON);
            btnAddImage.Size = new Size(50, 50);
            btnAddImage.Location = new Point(245, 35);
            btnAddImage.AutoSize = false;
            this.Controls.Add(btnAddImage);

            btnAddImage.Click += BtnAddImage_Click;

            btnAddImage.MouseHover += BtnAddImage_MouseHover;

            this.MouseDown += PlayerControl_MouseDown;
        }





        private void PlayerControl_MouseDown(object? sender, MouseEventArgs e)
        {
            PlayerControl? playerControl = sender as PlayerControl;
            if (playerControl == null)
            {
                return;
            }
            playerStartedDnD = playerControl;
            StartDnD(playerControl);
        }

        private void StartDnD(PlayerControl? playerControl)
        {
            if (playerControl is null)
            {
                return;
            }

            playerControl.DoDragDrop(playerControl, DragDropEffects.Move);
        }
        private void BtnAddImage_MouseHover(object? sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnAddImage, "Add image...");
        }

        private void BtnAddImage_Click(object? sender, EventArgs e)
        {
            using (var openImg = new OpenFileDialog())
            {
                openImg.Title = "Select image";
                openImg.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";

                if (openImg.ShowDialog() == DialogResult.OK)
                {
                    this.pbPlayerImage.Image = Image.FromFile(openImg.FileName);
                }

    }
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as PlayerControl);
        }

        public bool Equals(PlayerControl? other)
        {
            return other is not null &&
                   EqualityComparer<Label>.Default.Equals(lbName, other.lbName);
        }
    }
}
