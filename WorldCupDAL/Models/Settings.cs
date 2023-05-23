﻿using System;
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
        public static string CountryOne;
        public static int CountryOneId;
        public static string CountryTwo;
        public static int CountryTwoId;
        public static HashSet<string> Favourites;
    }
}