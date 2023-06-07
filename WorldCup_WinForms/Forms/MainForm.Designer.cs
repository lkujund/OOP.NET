namespace WorldCup_WinForms.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cbTeams = new System.Windows.Forms.ComboBox();
            this.pnlPlayers = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlFavourites = new System.Windows.Forms.FlowLayoutPanel();
            this.lbTeamPlayers = new System.Windows.Forms.Label();
            this.lbTeamFavourites = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.pnlPlayerRankings = new System.Windows.Forms.FlowLayoutPanel();
            this.lbPlayerRankings = new System.Windows.Forms.Label();
            this.pnlStadiumRankings = new System.Windows.Forms.FlowLayoutPanel();
            this.lbStadiumRankings = new System.Windows.Forms.Label();
            this.lbLoading = new System.Windows.Forms.Label();
            this.lbTeamPicker = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbYellowCards = new System.Windows.Forms.RadioButton();
            this.rbGoals = new System.Windows.Forms.RadioButton();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSettings
            // 
            resources.ApplyResources(this.btnSettings, "btnSettings");
            this.btnSettings.BackgroundImage = global::WorldCup_WinForms.Properties.Resources.settings_solid;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            this.btnSettings.MouseHover += new System.EventHandler(this.btnSettings_MouseHover);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.BackgroundImage = global::WorldCup_WinForms.Properties.Resources.print;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            this.btnPrint.MouseHover += new System.EventHandler(this.btnPrint_MouseHover);
            // 
            // cbTeams
            // 
            resources.ApplyResources(this.cbTeams, "cbTeams");
            this.cbTeams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTeams.FormattingEnabled = true;
            this.cbTeams.Name = "cbTeams";
            this.cbTeams.SelectedIndexChanged += new System.EventHandler(this.cbTeams_SelectedIndexChanged);
            // 
            // pnlPlayers
            // 
            resources.ApplyResources(this.pnlPlayers, "pnlPlayers");
            this.pnlPlayers.AllowDrop = true;
            this.pnlPlayers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(139)))), ((int)(((byte)(188)))));
            this.pnlPlayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlayers.Name = "pnlPlayers";
            this.pnlPlayers.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlPlayers_DragDrop);
            this.pnlPlayers.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlFavourites_DragEnter);
            this.pnlPlayers.DragLeave += new System.EventHandler(this.pnlPlayers_DragLeave);
            // 
            // pnlFavourites
            // 
            resources.ApplyResources(this.pnlFavourites, "pnlFavourites");
            this.pnlFavourites.AllowDrop = true;
            this.pnlFavourites.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.pnlFavourites.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFavourites.Name = "pnlFavourites";
            this.pnlFavourites.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlPlayers_DragDrop);
            this.pnlFavourites.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlFavourites_DragEnter);
            this.pnlFavourites.DragLeave += new System.EventHandler(this.pnlPlayers_DragLeave);
            // 
            // lbTeamPlayers
            // 
            resources.ApplyResources(this.lbTeamPlayers, "lbTeamPlayers");
            this.lbTeamPlayers.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lbTeamPlayers.Name = "lbTeamPlayers";
            // 
            // lbTeamFavourites
            // 
            resources.ApplyResources(this.lbTeamFavourites, "lbTeamFavourites");
            this.lbTeamFavourites.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lbTeamFavourites.Name = "lbTeamFavourites";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog1";
            resources.ApplyResources(this.openFileDialog2, "openFileDialog2");
            // 
            // pnlPlayerRankings
            // 
            resources.ApplyResources(this.pnlPlayerRankings, "pnlPlayerRankings");
            this.pnlPlayerRankings.AllowDrop = true;
            this.pnlPlayerRankings.BackColor = System.Drawing.Color.Teal;
            this.pnlPlayerRankings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlayerRankings.Name = "pnlPlayerRankings";
            // 
            // lbPlayerRankings
            // 
            resources.ApplyResources(this.lbPlayerRankings, "lbPlayerRankings");
            this.lbPlayerRankings.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lbPlayerRankings.Name = "lbPlayerRankings";
            // 
            // pnlStadiumRankings
            // 
            resources.ApplyResources(this.pnlStadiumRankings, "pnlStadiumRankings");
            this.pnlStadiumRankings.AllowDrop = true;
            this.pnlStadiumRankings.BackColor = System.Drawing.Color.YellowGreen;
            this.pnlStadiumRankings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStadiumRankings.Name = "pnlStadiumRankings";
            // 
            // lbStadiumRankings
            // 
            resources.ApplyResources(this.lbStadiumRankings, "lbStadiumRankings");
            this.lbStadiumRankings.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lbStadiumRankings.Name = "lbStadiumRankings";
            // 
            // lbLoading
            // 
            resources.ApplyResources(this.lbLoading, "lbLoading");
            this.lbLoading.Name = "lbLoading";
            // 
            // lbTeamPicker
            // 
            resources.ApplyResources(this.lbTeamPicker, "lbTeamPicker");
            this.lbTeamPicker.Name = "lbTeamPicker";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.rbYellowCards);
            this.panel1.Controls.Add(this.rbGoals);
            this.panel1.Name = "panel1";
            // 
            // rbYellowCards
            // 
            resources.ApplyResources(this.rbYellowCards, "rbYellowCards");
            this.rbYellowCards.Name = "rbYellowCards";
            this.rbYellowCards.UseVisualStyleBackColor = true;
            this.rbYellowCards.CheckedChanged += new System.EventHandler(this.rbYellowCards_CheckedChanged);
            // 
            // rbGoals
            // 
            resources.ApplyResources(this.rbGoals, "rbGoals");
            this.rbGoals.Checked = true;
            this.rbGoals.Name = "rbGoals";
            this.rbGoals.TabStop = true;
            this.rbGoals.UseVisualStyleBackColor = true;
            this.rbGoals.CheckedChanged += new System.EventHandler(this.rbGoals_CheckedChanged);
            // 
            // printPreviewDialog
            // 
            resources.ApplyResources(this.printPreviewDialog, "printPreviewDialog");
            this.printPreviewDialog.Document = this.printDocument;
            this.printPreviewDialog.Name = "printPreviewDialog";
            // 
            // printDocument
            // 
            this.printDocument.DocumentName = "rankings";
            this.printDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_EndPrint);
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // printDialog
            // 
            this.printDialog.Document = this.printDocument;
            this.printDialog.UseEXDialog = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbTeamPicker);
            this.Controls.Add(this.lbLoading);
            this.Controls.Add(this.lbStadiumRankings);
            this.Controls.Add(this.pnlStadiumRankings);
            this.Controls.Add(this.lbPlayerRankings);
            this.Controls.Add(this.pnlPlayerRankings);
            this.Controls.Add(this.lbTeamFavourites);
            this.Controls.Add(this.lbTeamPlayers);
            this.Controls.Add(this.pnlFavourites);
            this.Controls.Add(this.pnlPlayers);
            this.Controls.Add(this.cbTeams);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TextChanged += new System.EventHandler(this.MainForm_TextChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnSettings;
        private Button btnPrint;
        private ComboBox cbTeams;
        private Label lbTeamPlayers;
        private Label lbTeamFavourites;
        private OpenFileDialog openFileDialog1;
        private OpenFileDialog openFileDialog2;
        private FlowLayoutPanel pnlPlayerRankings;
        private Label lbPlayerRankings;
        private FlowLayoutPanel pnlStadiumRankings;
        private Label lbStadiumRankings;
        private Label lbLoading;
        private Label lbTeamPicker;
        public FlowLayoutPanel pnlPlayers;
        public FlowLayoutPanel pnlFavourites;
        private Panel panel1;
        private RadioButton rbYellowCards;
        private RadioButton rbGoals;
        private PrintPreviewDialog printPreviewDialog;
        private PrintDialog printDialog;
        private System.Drawing.Printing.PrintDocument printDocument;
    }
}