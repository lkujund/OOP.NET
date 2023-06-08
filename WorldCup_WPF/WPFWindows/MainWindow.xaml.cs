using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using WorldCup_WPF.Controls;
using WorldCup_WPF.WPFWindows;
using WorldCupDAL;
using WorldCupDAL.Models;

namespace WorldCup_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static HashSet<Team> _teams;
        private static HashSet<Match> _matches;
        private static List<Match> teamMatches = new();
        private static HashSet<Result> _results;
        public static Match theMatch;
        public MainWindow()
        {

            InitializeComponent();
            Repository.LoadSettings();
            if (Settings.WPFResolution == "1280x720")
            {
                Width = 1280;
                Height = 720;
            }
            else if (Settings.WPFResolution == "1600x900")
            {
                Width = 1600;
                Height= 900;
            }
            else if (Settings.WPFResolution == "1920x1080")
            {
                Width = 1920;
                Height = 1080;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                lbLoading.Foreground = new SolidColorBrush(Color.FromRgb(255,0,0));
                lbLoading.Content = "Loading...";

                btnPreviewTeam.IsEnabled = false;
                btnPreviewVersusTeam.IsEnabled = false;

                Repository.LoadSettings();
                Repository.ConfigLanguage();
                Repository.LoadFavourites();
                Repository.LoadImages();


                _teams = await Repository.LoadTeams();
                _matches = await Repository.LoadMatches();

                FillFirstComboBoxWithTeams();



            }
            catch (Exception)
            {

                MessageBox.Show("Data error!");
            }
        }

        private void FillFirstComboBoxWithTeams()
        {
            this.cbTeams.Items.Clear();
            foreach (Team team in _teams)
            {
                cbTeams.Items.Add($"{team.country} ({team.fifa_code})");
            }
            cbTeams.SelectedIndex = cbTeams.Items.IndexOf(Settings.CupGender == true ?
                Settings.CountrySelectedMale : Settings.CountrySelectedFemale);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CustomMessageBox msg= new CustomMessageBox();
            msg.ShowDialog();
            if (msg.DialogResult == true)
            {
                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void cbTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTeams.SelectedIndex != -1)
            {
                if (Settings.CupGender == true)
                {
                    Settings.CountrySelectedMale = cbTeams.SelectedValue.ToString();
                    Repository.SaveSettings();
                }
                else
                {
                    Settings.CountrySelectedFemale = cbTeams.SelectedValue.ToString();
                    Repository.SaveSettings();
                }
                FillSecondComboBoxWithTeams();
            }
        }

        private void FillSecondComboBoxWithTeams()
        {
            cbVersusTeams.Items.Clear();
            lbLoading.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            lbLoading.Content = "Loading...";

            teamMatches.Clear();
            string name = cbTeams.SelectedValue.ToString().Split("(")[0].Trim();
            teamMatches = _matches.Where(x => x.home_team.country == name || x.away_team.country == name).ToList();
            foreach (Match match in teamMatches)
            {
                if (match.home_team_country != name)
                {
                    cbVersusTeams.Items.Add((string)$"{match.home_team_country} ({_teams.FirstOrDefault(x => x.country == match.home_team_country).fifa_code})");
                }
                if (match.away_team_country != name)
                {
                    cbVersusTeams.Items.Add((string)$"{match.away_team_country} ({_teams.FirstOrDefault(x => x.country == match.away_team_country).fifa_code})");
                }
            }
            lbLoading.Foreground = new SolidColorBrush(Color.FromRgb(0, 100, 0));
            lbLoading.Content = "Data loaded.";

            cbVersusTeams.SelectedValue = Settings.WPFSecondCountry;
            if (cbVersusTeams.SelectedIndex == -1)
            {
                lbLoading.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                lbLoading.Content = "Waiting on team two...";
            }

        }

        private void cbVersusTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbLoading.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            lbLoading.Content = "Loading...";
            if (cbVersusTeams.SelectedIndex != -1)
            {


                Settings.WPFSecondCountry = cbVersusTeams.SelectedValue.ToString();
                Repository.SaveSettings();

                string team1 = cbTeams.SelectedValue.ToString().Split("(")[0].Trim();
                string team2 = cbVersusTeams.SelectedValue.ToString().Split("(")[0].Trim();

                theMatch = teamMatches.FirstOrDefault(x => 
                (x.home_team_country == team1 && x.away_team_country == team2) ||
                (x.home_team_country == team2 && x.away_team_country == team1));

                btnPreviewTeam.IsEnabled = true;
                btnPreviewVersusTeam.IsEnabled = true;

                DrawMatchResults();
                RenderPlayingField();
            }
        }

        private async void DrawMatchResults()
        {
            _results = await Repository.LoadResults();

            lbFirstTeamName.Content = theMatch.home_team_country;
            lbFirstTeamResult.Content = theMatch.home_team.goals.ToString();

            lbSecondTeamName.Content = theMatch.away_team_country;
            lbSecondTeamResult.Content = theMatch.away_team.goals.ToString();
        }
        private void RenderPlayingField()
        {
            List<Player> homePlayers = theMatch.home_team_statistics.starting_eleven;
            List<Player> awayPlayers = theMatch.away_team_statistics.starting_eleven;

            //draw home players
            homeGoalie.Children.Clear();
            homeDefender.Children.Clear();
            homeMidfield.Children.Clear();
            homeForward.Children.Clear();

            foreach (Player player in homePlayers)
            {
                PlayerControl playerControl = new PlayerControl(player);
                    
                switch (player.position)
                {
                    case "Goalie":
                        homeGoalie.Children.Add(playerControl);
                        break;
                    case "Defender":
                        homeDefender.Children.Add(playerControl);
                        break;
                    case "Midfield":
                        homeMidfield.Children.Add(playerControl);
                        break;
                    case "Forward":
                        homeForward.Children.Add(playerControl);
                        break;
                    default: 
                        break;
                }
            }

            //draw away players
            awayGoalie.Children.Clear();
            awayDefender.Children.Clear();
            awayMidfield.Children.Clear();
            awayForward.Children.Clear();
            foreach (Player player in awayPlayers)
            {
                PlayerControl playerControl = new PlayerControl(player);

                switch (player.position)
                {
                    case "Goalie":
                        awayGoalie.Children.Add(playerControl);
                        break;
                    case "Defender":
                        awayDefender.Children.Add(playerControl);
                        break;
                    case "Midfield":
                        awayMidfield.Children.Add(playerControl);
                        break;
                    case "Forward":
                        awayForward.Children.Add(playerControl);
                        break;
                    default:
                        break;
                }
            }

            lbLoading.Foreground = new SolidColorBrush(Color.FromRgb(0, 100, 0));
            lbLoading.Content = "Data loaded.";
        }


        private void btnPreviewTeam_Click(object sender, RoutedEventArgs e)
        {
            Result result = _results.FirstOrDefault(x => x.country == cbTeams.Text.ToString().Split("(")[0].Trim());
            new TeamInfoWindow(result).Show();
        }

        private void btnPreviewVersusTeam_Click(object sender, RoutedEventArgs e)
        {
            Result result = _results.FirstOrDefault(x => x.country == cbVersusTeams.Text.ToString().Split("(")[0].Trim());
            new TeamInfoWindow(result).Show();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new ConfigWindow().Show();
        }
    }
}
