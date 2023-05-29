using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupDAL;
using WorldCupDAL.Models;

namespace WorldCup_WinForms.Forms
{
    public partial class MainForm : Form
    {
        private static HashSet<Team> _teams;
        private static HashSet<Match> _matches;
        private static HashSet<Result> _results;
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Hide();
            new ConfigForm().Show();
            
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Repository.LoadSettings();
                Repository.ConfigLanguage();
                _teams = await Repository.LoadTeams();
                _matches = await Repository.LoadMatches();
                _results = await Repository.LoadResults();

                FillComboBoxWithTeams();
            }
            catch (Exception)
            {

                MessageBox.Show("Data error!");
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

        private void cbTeams_SelectedIndexChanged(object sender, EventArgs e)
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
        }
    }
}
