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

namespace WorldCup_WinForms.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Hide();
            new ConfigForm().Show();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Repository.LoadSettings();
            Repository.ConfigLanguage();
        }
    }
}
