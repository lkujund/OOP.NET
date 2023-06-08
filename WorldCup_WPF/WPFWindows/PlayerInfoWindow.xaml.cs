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
using WorldCupDAL;
using WorldCupDAL.Models;

namespace WorldCup_WPF.WPFWindows
{
    /// <summary>
    /// Interaction logic for PlayerInfoWindow.xaml
    /// </summary>
    public partial class PlayerInfoWindow : Window
    {
        public PlayerInfoWindow(Player player, ImageSource source, int goals, int cards)
        {
            InitializeComponent();

            playerImage.Source= source;
            playerImage.Stretch = Stretch.Fill;
            playerImage.Width = playerImage.Height = 180;



            playerName.Content = player.name;
            playerNumber.Content = player.shirt_number;
            playerPosition.Content = player.position;
            if (Settings.Language == Repository.HR || Settings.Language == null)
            {
                playerCaptain.Content = player.captain ? "DA" : "NE"; 
            }
            else
            {
                playerCaptain.Content = player.captain ? "YES" : "NO";
            }
            goalNumber.Content = goals.ToString();
            yellowCards.Content = cards.ToString();
        }
    }
}
