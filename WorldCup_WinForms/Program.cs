using WorldCup_WinForms.Forms;
using WorldCupDAL;

namespace WorldCup_WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            if (!Repository.IsConfigured())
            {
                Application.Run(new ConfigForm()); 
            }else
            {
                Application.Run(new MainForm());
            }
            
        }
    }
}