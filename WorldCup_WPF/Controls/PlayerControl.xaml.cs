using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using WorldCup_WPF.WPFWindows;
using WorldCupDAL;
using WorldCupDAL.Models;

namespace WorldCup_WPF.Controls
{
    /// <summary>
    /// Interaction logic for PlayerControl.xaml
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        public PlayerControl(Player player)
        {
            InitializeComponent();

            playerName.Content = player.name;
            playerNumber.Content = player.shirt_number.ToString();
            try
            {
                string imgFile = Repository._playerimages.GetValueOrDefault($"Name: {player.name}");
                if (File.Exists(imgFile))
                {
                    playerImage.Source = new BitmapImage(new Uri(Uri.UnescapeDataString(imgFile)));
                }
                else
                {
                    playerImage.Source = new BitmapImage(new Uri(Uri.UnescapeDataString(Repository.DEFAULT_IMAGE)));
                }
                playerImage.Stretch = Stretch.Fill;
                playerImage.Width = playerImage.Height = 110;
            }
            catch (Exception)
            {
                playerImage.Source = new BitmapImage(new Uri(Uri.UnescapeDataString(Repository.DEFAULT_IMAGE)));
                MessageBox.Show("Image error!", "Error");
                playerImage.Width = playerImage.Height = 110;
            }

        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            PlayerControl playerControl = (sender as Grid).Parent as PlayerControl;
            List<Player> players = new();
            players.AddRange(MainWindow.theMatch.home_team_statistics.starting_eleven);
            players.AddRange(MainWindow.theMatch.away_team_statistics.starting_eleven);
            Player player = players.FirstOrDefault(x => x.name.ToString() == playerControl.playerName.Content.ToString());

            List<Event> events = new List<Event>();
            events.AddRange(MainWindow.theMatch.home_team_events);
            events.AddRange(MainWindow.theMatch.away_team_events);
            int goals = 0;
            int cards = 0;

            foreach (Event ev in events)
            {
                if (ev.type_of_event == "yellow-card" && ev.player == player.name)
                {
                    cards++;
                }
                if (ev.type_of_event.Contains("goal") && ev.player == player.name)
                {
                    goals++;
                }
            }

            new PlayerInfoWindow(player, playerImage.Source, goals, cards).Show();
        }
    }
}
