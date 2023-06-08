using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupDAL.Models
{
    public static class Settings
    {
        public static string Language;
        public static bool CupGender; // false - female, true - male
        public static string CountrySelectedMale;
        public static string CountrySelectedFemale;
        public static HashSet<string> Favourites = new();
        public static string WPFSecondCountry = "";
        public static string WPFResolution = "";
    }
}
