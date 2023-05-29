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
            Repository.LoadSettings();
            Repository.ConfigLanguage();
            _teams = await Repository.LoadTeams();
        }
    }
}
