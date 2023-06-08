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
using WorldCupDAL;

namespace WorldCup_WPF.WPFWindows
{
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        private static int oneHidden = 0;
        public ConfigWindow()
        {
            InitializeComponent();
            Repository.ConfigLanguage();
            if (Settings.Language == null)
            {
                btnCancel.IsEnabled = false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (cbLanguage.SelectedItem != null &&
                cbResolution.SelectedItem != null &&
                (rbFemale.IsChecked == true || rbMale.IsChecked == true))
            {
                CustomMessageBox messageBox = new CustomMessageBox();
                messageBox.ShowDialog();
                if (messageBox.DialogResult == true)
                {
                    SetPreferences(cbLanguage, rbFemale);
                    Repository.SaveSettings();
                    Repository.ConfigLanguage();
                    if (oneHidden == 0)
                    {
                        oneHidden = 1;
                        Hide();
                        new MainWindow().Show();
                    }
                    else
                    {
                        Close();
                        new MainWindow().Show();
                    }
                }
            }
            else
            {

                MessageBox.Show(
                    Settings.Language == Repository.EN ?
                    "Error: You must choose a language, resolution and the cup!" :
                    "Greška: Potrebno je odabrati jezik, rezoluciju i prvenstvo!");
            }
        }

        private void SetPreferences(ComboBox cbLanguage, RadioButton rbFemale)
        {
            string lang = cbLanguage.SelectedItem.ToString().Split(" ")[1].Trim();
            if (lang == "Hrvatski" ||
                lang == "Croatian")
            {
                Settings.Language = Repository.HR;
            }
            else
            {
                Settings.Language = Repository.EN;
            }

            if (rbFemale.IsChecked == true)
            {
                Settings.CupGender = false;
            }
            else
            {
                Settings.CupGender = true;
            }

            if (cbResolution.SelectedIndex == 3)
            {
                Settings.WPFResolution = "FullscreenxFullscreen";
            }
            else
            {
                Settings.WPFResolution = cbResolution.SelectedItem.ToString().Split(" ")[1]; 
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Repository.ConfigLanguage();
            if (oneHidden == 0)
            {
                oneHidden = 1;
                Hide(); 
                new MainWindow().Show();
            }
            else
            {
                Close();
                new MainWindow().Show();
            }
        }


    }


}


