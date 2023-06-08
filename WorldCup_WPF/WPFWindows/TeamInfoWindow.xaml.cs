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
using System.Windows.Shapes;
using WorldCupDAL.Models;

namespace WorldCup_WPF.WPFWindows
{
    /// <summary>
    /// Interaction logic for TeamInfoWindow.xaml
    /// </summary>
    public partial class TeamInfoWindow : Window
    {
        public TeamInfoWindow(Result teamResult)
        {
            InitializeComponent();
            countryName.Content = teamResult.country;
            countryFifaCode.Content = teamResult.fifa_code;
            gamesPlayed.Content = teamResult.games_played.ToString();
            gamesWon.Content = teamResult.wins.ToString();
            gamesLost.Content = teamResult.losses.ToString();
            gamesDraw.Content = teamResult.draws.ToString();
            goalsScored.Content = teamResult.goals_for.ToString();
            goalsReceived.Content = teamResult.goals_against.ToString();
            goalDifference.Content = teamResult.goal_differential.ToString();
            if (teamResult.goals_for >= teamResult.goals_against)
            {
                goalDifference.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }
            else
            {
                goalDifference.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
