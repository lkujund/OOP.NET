using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupDAL.Models;

namespace WorldCupDAL
{
    public class Repository
    {
        public static List<string> _settings = new List<string>();

        public const string HR = "hr", EN = "en";
        public static string SETTINGS = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "WorldCupDAL\\Data\\settings.txt");
        public static string FAVOURITES = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "WorldCupDAL\\Data\\favourites.txt");
        public const string DEFAULTSETTINGS = "Croatian|True|";
        private const char SEPARATOR = '|';


        public static bool IsConfigured()
        {
            if (File.Exists(SETTINGS))
            {
                //provjeri
                var settings = File.ReadAllLines(SETTINGS);
                var data = settings[0].Split(SEPARATOR);
                if (data.Length == 6)
                {
                    return true;
                }
                else 
                    return false;
            }
            else
                return false;

        }
        public static void ConfigLanguage()
        {
            if (Settings.Language == HR || Settings.Language == null)
            {
                ConfigCulture(HR);
            }else
            {
                ConfigCulture(EN);
            }
        }
        private static void ConfigCulture(string lang)
        {
            CultureInfo culture = new (lang);

            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }


        public static void SaveSettings()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder
                .Append(Settings.Language).Append(SEPARATOR)
                .Append(Settings.CupGender).Append(SEPARATOR)
                .Append(Settings.CountryOne).Append(SEPARATOR)
                .Append(Settings.CountryOneId).Append(SEPARATOR)
                .Append(Settings.CountryTwo).Append(SEPARATOR)
                .Append(Settings.CountryTwoId);

            File.WriteAllText(SETTINGS, stringBuilder.ToString());
                            
        }

        public static void LoadSettings()
        {
            if (File.Exists(SETTINGS))
            {
                var file = File.ReadAllLines(SETTINGS);
                _settings = file[0].Split(SEPARATOR).ToList();
                Settings.Language = _settings[0];
                Settings.CupGender = Boolean.Parse(_settings[1]);
                Settings.CountryOne = _settings[2];
                Settings.CountryOneId = int.Parse(_settings[3]);
                Settings.CountryTwo = _settings[4];
                Settings.CountryTwoId = int.Parse(_settings[5]);

            }
        }

        public static void SaveFavourites()
        {

        }

        public static void LoadFavourites()
        {

        }

        public static void LoadTeams()
        {

        }

        public static void LoadMatches()
        {

        }
        public static void LoadResults()
        {

        }
    }
}
