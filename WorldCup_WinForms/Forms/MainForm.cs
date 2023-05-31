using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        private List<PlayerRankControl> rankControls = new();
        private List<string> favourites= new List<string>();

        private static HashSet<Team> _teams;
        private static HashSet<Match> _matches;
        private static HashSet<Result> _results;
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

            new ConfigForm().Show();
            this.Hide();
            
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
                foreach (Player player in players)
                {
                    PlayerControl playerControl = new PlayerControl();
                    playerControl.lbName.Text = "Name: " + player.name;
                    playerControl.lbNumber.Text = "Number: " + player.shirt_number.ToString();
                    playerControl.lbPosition.Text = "Position: " + player.position;
                    playerControl.lbCaptain.Visible = player.captain;
                    pnlPlayers.Controls.Add(playerControl);

                }



                //load player rankings
                List<Match>? matches = _matches.Where(x => x.home_team.country == name || x.away_team.country == name).ToList();
                List<Event>? events = new();
                List<PlayerRankControl> ranks = new();

                foreach (Match m in matches)
                {
                    events.AddRange(m.home_team_events);
                    events.AddRange(m.away_team_events);
                }

                foreach (Player player in players)
                {
                    int goals = events.Where(x => x.player == player.name && x.type_of_event == "goal").Count();
                    int yellowCards = events.Where(x => x.player == player.name && x.type_of_event == "yellow-card").Count();

                    PlayerRankControl playerRank = new PlayerRankControl();
                    playerRank.lbName.Text = "Name: " + player.name;
                    playerRank.lbGoalNumber.Text = "Goals: " + goals.ToString();
                    playerRank.lbYellowCards.Text = "Yellow cards: " + yellowCards.ToString();
                    playerRank.pbPlayerImage.Image = pnlPlayers.Controls.OfType<PlayerControl>().FirstOrDefault(x => x.lbName.Text == playerRank.lbName.Text).pbPlayerImage.Image;

                    ranks.Add(playerRank);
                }
                ranks.Sort();

                foreach (PlayerRankControl rank in ranks)
                {

                    pnlPlayerRankings.Controls.Add(rank);
                }




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

        private void pnlPlayers_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pnlPlayers_DragOver(object sender, DragEventArgs e)
        {

        }

        private void pnlPlayers_DragDrop(object sender, DragEventArgs e)
        {

        }
    }
}
