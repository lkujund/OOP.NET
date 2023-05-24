using WorldCup_WinForms.Forms;
using WorldCupDAL;
using WorldCupDAL.Models;

namespace WorldCup_WinForms
{
    public partial class ConfigForm : Form
    {
        
        public ConfigForm()
        {
            InitializeComponent();
            Repository.ConfigLanguage();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbLanguage.SelectedItem != null &&
                (rbFemale.Checked || rbMale.Checked))
            {
                SetPreferences(cbLanguage, rbFemale);
                Repository.SaveSettings();
                Hide();
                new MainForm().Show(); 
            }
            else
            {
                
                MessageBox.Show(
                    Settings.Language == Repository.EN ?
                    "Error: You must choose both the language and the cup!" :
                    "Greška: Potrebno je odabrati i jezik i prvenstvo!");
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
            new MainForm().Show();
        }
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            Repository.LoadSettings();
            Repository.ConfigLanguage();
            if (Settings.Language == null)            
            {
                btnCancel.Enabled = false;
            }
        }
        private void SetPreferences(ComboBox cbLanguage, RadioButton rbFemale)
        {
            if (cbLanguage.SelectedItem.ToString() == "Hrvatski" ||
                cbLanguage.SelectedItem.ToString() == "Croatian")
            {
                Settings.Language = Repository.HR;
            }
            else
            {
                Settings.Language = Repository.EN;
            }

            if (rbFemale.Checked)
            {
                Settings.CupGender = true;
            }
            else
            {
                Settings.CupGender = false;
            }
        }

    }
}