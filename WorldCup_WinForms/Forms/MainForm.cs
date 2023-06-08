using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCup_WinForms.Panels;
using WorldCupDAL;
using WorldCupDAL.Models;

namespace WorldCup_WinForms.Forms
{   
    public partial class MainForm : Form
    {

        private int printedPages;

        private List<PlayerRankControl> rankControls = new();
        private List<string> favourites= new List<string>();

        public static PlayerControl lastPlayerImg;

        private static HashSet<Team> _teams;
        private static HashSet<Match> _matches;
        private static HashSet<Result> _results;
        private static readonly Color backColor = SystemColors.GradientActiveCaption;

        public delegate void ImgChanged();

        public MainForm()
        {
            InitializeComponent();
            printedPages = 0;
        }



        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                lbLoading.ForeColor = Color.Red;
                lbLoading.Text = "Loading...";

                Repository.LoadSettings();
                Repository.ConfigLanguage();
                Repository.LoadFavourites();
                Repository.LoadImages();

                _teams = await Repository.LoadTeams();
                _matches = await Repository.LoadMatches();
                
                FillComboBoxWithTeams();
                LoadTeamData();


            }
            catch (Exception)
            {

                MessageBox.Show("Data error!");
            }
        }

        private async void LoadTeamData()
        {
            lbLoading.ForeColor = Color.Red;
            lbLoading.Text = "Loading...";

            _results = await Repository.LoadResults();



            if (cbTeams.SelectedIndex < 0)
            {
                return;
            }
            else
            {
                pnlPlayers.Controls.Clear();
                pnlFavourites.Controls.Clear();
                pnlPlayerRankings.Controls.Clear();
                pnlStadiumRankings.Controls.Clear();




                //load players
                string name = cbTeams.SelectedItem.ToString().Split("(")[0].Trim();
                Match? match = _matches.FirstOrDefault(x => x.home_team.country == name || x.away_team.country == name);
                List<Player> players = new List<Player>();
                players.AddRange(match.home_team_statistics.starting_eleven);
                players.AddRange(match.home_team_statistics.substitutes);
                rankPlayers.Clear();
                rankPlayers = players;
                foreach (Player player in players)
                {
                    PlayerControl playerControl = new PlayerControl();
                    playerControl.lbName.Text = "Name: " + player.name;
                    playerControl.lbNumber.Text = "Number: " + player.shirt_number.ToString();
                    playerControl.lbPosition.Text = "Position: " + player.position;
                    playerControl.lbCaptain.Visible = player.captain;
                    try
                    {
                        string imgFile = Repository._playerimages.GetValueOrDefault(playerControl.lbName.Text);
                        if (File.Exists(imgFile))
                        {
                            playerControl.pbPlayerImage.Image = Image.FromFile(imgFile);
                        }
                    }
                    catch (Exception)
                    {

                        playerControl.pbPlayerImage.Image = Image.FromFile(Repository.DEFAULT_IMAGE);
                        MessageBox.Show("Image error!", "Error");
                    }
                    pnlPlayers.Controls.Add(playerControl);

                }



                //load player rankings
                List<Match>? matches = _matches.Where(x => x.home_team.country == name || x.away_team.country == name).ToList();
                rankMatch.Clear();
                rankMatch = matches;
                List<Event>? events = new();
                List<PlayerRankControl> ranks = new();

                LoadPlayerRankings(rankMatch, events, ranks, rankPlayers);






                //load favourites
                if (Settings.Favourites.Count != 0)
                {
                    favourites = Settings.Favourites.ToList();
                    foreach (string playerName in favourites)
                    {
                        PlayerControl? player = pnlPlayers.Controls.OfType<PlayerControl>().FirstOrDefault(x => x.lbName.Text == "Name: " + playerName);
                        if (player != null && !pnlFavourites.Controls.OfType<PlayerControl>().Contains(player))
                        {
                            pnlFavourites.Controls.Add(player);
                            pnlPlayers.Controls.Remove(player);
                            player.lbFavouriteStar.Visible = true;

                        }
                    }
                }






                //load stadium rankings
                matches.Sort((x,y) => -int.Parse(x.attendance).CompareTo(int.Parse(y.attendance)));

                foreach (Match m in matches)
                {
                    StadiumControl stadiumControl = new();
                    stadiumControl.lbLocation.Text = "Location: " + m.location;
                    stadiumControl.lbAttendance.Text = "Attendance: " + m.attendance;
                    stadiumControl.lbHomeTeam.Text = "Home: " + m.home_team_country;
                    stadiumControl.lbAwayTeam.Text = "Away: " + m.away_team_country;

                    pnlStadiumRankings.Controls.Add(stadiumControl);
                }


                lbLoading.ForeColor = Color.Green;
                lbLoading.Text = "Data loaded.";
            }
        }

        //static tables for rank sorting
        private static List<Match> rankMatch = new();

        private static List<Player> rankPlayers = new();
        public void LoadPlayerRankings(List<Match> matches, List<Event> events, List<PlayerRankControl> ranks, List<Player> players)
        {
            pnlPlayerRankings.Controls.Clear();
            foreach (Match m in matches)
            {
                events.AddRange(m.home_team_events);
                events.AddRange(m.away_team_events);
            }

            foreach (Player player in players)
            {
                int goals = events.Where(x => x.player == player.name && x.type_of_event.Contains("goal")).Count();
                int yellowCards = events.Where(x => x.player == player.name && x.type_of_event == "yellow-card").Count();

                PlayerRankControl playerRank = new PlayerRankControl();
                playerRank.lbName.Text = "Name: " + player.name;
                playerRank.lbGoalNumber.Text = "Goals: " + goals.ToString();
                playerRank.lbYellowCards.Text = "Yellow cards: " + yellowCards.ToString();
                if (pnlPlayers.Controls.OfType<PlayerControl>().Any(x => x.lbName.Text == playerRank.lbName.Text))
                {
                    playerRank.pbPlayerImage.Image = pnlPlayers.Controls.OfType<PlayerControl>().FirstOrDefault(x => x.lbName.Text == playerRank.lbName.Text).pbPlayerImage.Image; 
                }

                if (pnlFavourites.Controls.OfType<PlayerControl>().Any(x => x.lbName.Text == playerRank.lbName.Text))
                {
                    playerRank.pbPlayerImage.Image = pnlFavourites.Controls.OfType<PlayerControl>().FirstOrDefault(x => x.lbName.Text == playerRank.lbName.Text).pbPlayerImage.Image;
                }

                ranks.Add(playerRank);
            }
            if (rbGoals.Checked == true)
            {
                ranks.Sort((p1, p2) => -p1.lbGoalNumber.Text.CompareTo(p2.lbGoalNumber.Text));
            }
            if (rbYellowCards.Checked == true)
            {
                ranks.Sort((p1, p2) => -p1.lbYellowCards.Text.CompareTo(p2.lbYellowCards.Text));         
            }

            foreach (PlayerRankControl rank in ranks)
            {

                pnlPlayerRankings.Controls.Add(rank);
            }
        }

        private void FillComboBoxWithTeams()
        {
            this.cbTeams.Items.Clear();
            foreach (Team team in _teams)
            {
                cbTeams.Items.Add($"{team.country} ({team.fifa_code})");
            }
            cbTeams.SelectedIndex = cbTeams.FindString(Settings.CupGender == true ?
                Settings.CountrySelectedMale : Settings.CountrySelectedFemale);
        }

        private async void cbTeams_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Settings.CupGender == true)
            {
                Settings.CountrySelectedMale = cbTeams.SelectedItem.ToString();
                Repository.SaveSettings();
            }
            else
            {
                Settings.CountrySelectedFemale = cbTeams.SelectedItem.ToString();
                Repository.SaveSettings();
            }


            LoadTeamData();
            

            

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CustomMessageBox mbox = new CustomMessageBox();
            mbox.ShowDialog();
            if (mbox.DialogResult == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {

            new ConfigForm().Show();
            this.Hide();
            
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                if (printPreviewDialog.ShowDialog() == DialogResult.Yes)
                {
                    printDocument.Print();
                }
            }
        }

        private void btnSettings_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnSettings, Settings.Language == Repository.EN ? "Settings..." : "Postavke...");
        }
        private void btnPrint_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnPrint, Settings.Language == Repository.EN ? "Print rankings..." : "Ispiši rang liste...");
        }




        private void pnlFavourites_DragEnter(object sender, DragEventArgs e)
        {
            FlowLayoutPanel? panel = sender as FlowLayoutPanel;

            if (panel == null)
            {
                return;
            }

            e.Effect = DragDropEffects.Move;

        }

        private void pnlPlayers_DragLeave(object sender, EventArgs e)
        {
            FlowLayoutPanel? panel = sender as FlowLayoutPanel;
            if (panel == null)
            {
                return;
            }


        }

        private void pnlPlayers_DragDrop(object sender, DragEventArgs e)
        {

            FlowLayoutPanel? panel = sender as FlowLayoutPanel;
            if (panel == null)
            {
                return;
            }

            PlayerControl? playerControl = e.Data.GetData(typeof(PlayerControl)) as PlayerControl;

            if (panel.Name == "pnlFavourites")
            {
                if (panel.Controls.Count >= 3)
                {
                    return;
                }
                PlayerControl target = pnlPlayers.Controls.OfType<PlayerControl>().FirstOrDefault(x => x.lbName.Text == playerControl.lbName.Text);
                pnlPlayers.Controls.Remove(target);
                playerControl.lbFavouriteStar.Visible = true;
                pnlFavourites.Controls.Add(playerControl);
                string addFavourite = playerControl.lbName.Text.Split(":")[1].Trim();
                Settings.Favourites.Add(addFavourite);
                Repository.SaveFavourites();
            }
            else
            {
                PlayerControl target = pnlFavourites.Controls.OfType<PlayerControl>().FirstOrDefault(x => x.lbName.Text == playerControl.lbName.Text);
                pnlFavourites.Controls.Remove(target);
                playerControl.lbFavouriteStar.Visible = false;
                pnlPlayers.Controls.Add(playerControl);
                string removeFavourite = playerControl.lbName.Text.Split(":")[1].Trim();
                Settings.Favourites.Remove(removeFavourite);
                Repository.SaveFavourites();
            }
        }

        private void MainForm_TextChanged(object sender, EventArgs e)
        {
            var target = pnlPlayerRankings.Controls.OfType<PlayerRankControl>().FirstOrDefault(x => x.lbName.Text == lastPlayerImg.lbName.Text);
            target.pbPlayerImage.Image = lastPlayerImg.pbPlayerImage.Image;

        }

        private void rbGoals_CheckedChanged(object sender, EventArgs e)
        {
            List<Event> events = new();
            List<PlayerRankControl> ranks = new();
            LoadPlayerRankings(rankMatch, events, ranks, rankPlayers);
        }

        private void rbYellowCards_CheckedChanged(object sender, EventArgs e)
        {
            List<Event> events = new();
            List<PlayerRankControl> ranks = new();
            LoadPlayerRankings(rankMatch, events, ranks, rankPlayers);
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {


            FlowLayoutPanel playerRanks = pnlPlayerRankings;
            FlowLayoutPanel stadiumRanks = pnlStadiumRankings;

            //print player ranks
            if (printedPages < 1)
            {
                for (int i = 0; i < playerRanks.Controls.Count / 2; i++)
                {
                    Control playerRank = playerRanks.Controls[i];

                    int x = e.MarginBounds.X;
                    int y = e.MarginBounds.Y + (i) * playerRank.Height;

                    playerRank.Location = new Point(x, y);

                    var bmp = new Bitmap(playerRank.Width, playerRank.Height);
                    playerRank.DrawToBitmap(bmp, new Rectangle(0, 0, playerRank.Width, playerRank.Height));

                    e.Graphics.DrawImage(bmp, x, y);
                }

                for (int i = playerRanks.Controls.Count / 2; i < playerRanks.Controls.Count; i++)
                {
                    Control playerRank = playerRanks.Controls[i];

                    int x = e.MarginBounds.X + playerRank.Width;
                    int y = e.MarginBounds.Y + (i - playerRanks.Controls.Count / 2) * playerRank.Height;

                    playerRank.Location = new Point(x, y);

                    var bmp = new Bitmap(playerRank.Width, playerRank.Height);
                    playerRank.DrawToBitmap(bmp, new Rectangle(0, 0, playerRank.Width, playerRank.Height));

                    e.Graphics.DrawImage(bmp, x, y);
                }

            }


            //print stadium ranks

            if (printedPages == 1)
            {
                for (int i = 0; i < stadiumRanks.Controls.Count; i++)
                {
                    Control stadiumRank = stadiumRanks.Controls[i];

                    int x = e.MarginBounds.X;
                    int y = e.MarginBounds.Y + (i) * stadiumRank.Height;

                    stadiumRank.Location = new Point(x, y);

                    var bmp = new Bitmap(stadiumRank.Width, stadiumRank.Height);
                    stadiumRank.DrawToBitmap(bmp, new Rectangle(0, 0, stadiumRank.Width, stadiumRank.Height));

                    e.Graphics.DrawImage(bmp, x, y);
                } 
            }


            if (++printedPages < 2)
            {
                e.HasMorePages = true;
            }
            else
            {
                printedPages = 0;
            }

        }

        private void printDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (e.PrintAction == PrintAction.PrintToPrinter)
            {
                MessageBox.Show(Settings.Language == Repository.EN ?
                    "Printing finished." :
                    "Printanje dovršeno.");
            }
        }
    }
}
