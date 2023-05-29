﻿using Newtonsoft.Json;
using RestSharp;
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

        //men cup paths
        public static string JSON_MALE_TEAMS = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "WorldCupDAL\\Data\\JSON\\men\\teams.json");
        public static string JSON_MALE_RESULTS = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "WorldCupDAL\\Data\\JSON\\men\\results.json");
        public static string JSON_MALE_GROUP_RESULTS = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "WorldCupDAL\\Data\\JSON\\men\\group_results.json");
        public static string JSON_MALE_MATCHES = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "WorldCupDAL\\Data\\JSON\\men\\matches.json");

        public static string WEB_MALE_TEAMS = "https://worldcup-vua.nullbit.hr/men/teams";
        public static string WEB_MALE_MATCHES = "https://worldcup-vua.nullbit.hr/men/teams/matches";
        public static string WEB_MALE_RESULTS = "https://worldcup-vua.nullbit.hr/men/teams/results";
        public static string WEB_MALE_GROUP_RESULTS = "https://worldcup-vua.nullbit.hr/men/teams/group_results";

        //women cup paths
        public static string JSON_FEMALE_TEAMS = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "WorldCupDAL\\Data\\JSON\\women\\teams.json");
        public static string JSON_FEMALE_RESULTS = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "WorldCupDAL\\Data\\JSON\\women\\results.json");
        public static string JSON_FEMALE_GROUP_RESULTS = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "WorldCupDAL\\Data\\JSON\\women\\group_results.json");
        public static string JSON_FEMALE_MATCHES = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "WorldCupDAL\\Data\\JSON\\women\\matches.json");

        public static string WEB_FEMALE_TEAMS = "https://worldcup-vua.nullbit.hr/women/teams";
        public static string WEB_FEMALE_MATCHES = "https://worldcup-vua.nullbit.hr/women/teams/matches";
        public static string WEB_FEMALE_RESULTS = "https://worldcup-vua.nullbit.hr/women/teams/results";
        public static string WEB_FEMALE_GROUP_RESULTS = "https://worldcup-vua.nullbit.hr/women/teams/group_results";




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
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder
                .Append(Settings.Language).Append(SEPARATOR)
                .Append(Settings.CupGender).Append(SEPARATOR)
                .Append(Settings.CountryOne).Append(SEPARATOR)
                .Append(Settings.CountryOneId).Append(SEPARATOR)
                .Append(Settings.CountryTwo).Append(SEPARATOR)
                .Append(Settings.CountryTwoId);

            File.WriteAllText(FAVOURITES, stringBuilder.ToString());
        }

        public static void LoadFavourites()
        {
            if (File.Exists(FAVOURITES))
            {

            }
        }

        public static Task<HashSet<Team>?> LoadTeams()
        {
            if (File.Exists(JSON_MALE_TEAMS) && File.Exists(JSON_FEMALE_TEAMS))
            {
                return Task.Run
                (() =>
                {
                    StreamReader reader = new StreamReader(
                        Settings.CupGender == true ? 
                        JSON_MALE_TEAMS : JSON_FEMALE_TEAMS); //male = true, female = false
                        
                    string teams = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<HashSet<Team>>(teams);
                        
                    });
                

            }
            else
            {
                return Task.Run
                (() =>
                {
                        RestClient restClient = new RestClient(
                            Settings.CupGender == true ?
                            WEB_MALE_TEAMS : WEB_FEMALE_TEAMS); //male = true, female = false

                        RestResponse restResponse = restClient.Execute<HashSet<Team>>(new RestRequest());
                    File.WriteAllText(Settings.CupGender == true ?
                        JSON_MALE_TEAMS : JSON_FEMALE_TEAMS, restResponse.Content.ToString()); //napravi lokalni file
                    return JsonConvert.DeserializeObject<HashSet<Team>>(restResponse.Content);
                });
            }
        }

        public static Task<HashSet<Match>?> LoadMatches()
        {
            if (File.Exists(JSON_MALE_MATCHES) && File.Exists(JSON_FEMALE_MATCHES))
            {
                return Task.Run
                (() =>
                {
                    StreamReader reader = new StreamReader(
                        Settings.CupGender == true ?
                        JSON_MALE_MATCHES : JSON_FEMALE_MATCHES); //male = true, female = false

                    string matches = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<HashSet<Match>>(matches);

                });


            }
            else
            {
                return Task.Run
                (() =>
                {
                    RestClient restClient = new RestClient(
                        Settings.CupGender == true ?
                        WEB_MALE_MATCHES : WEB_FEMALE_MATCHES); //male = true, female = false

                    RestResponse restResponse = restClient.Execute<HashSet<Match>>(new RestRequest());
                    File.WriteAllText(Settings.CupGender == true ?
                        JSON_MALE_MATCHES : JSON_FEMALE_MATCHES, restResponse.Content.ToString()); //napravi lokalni file
                    return JsonConvert.DeserializeObject<HashSet<Match>>(restResponse.Content);
                });
            }
        }
        public static Task<HashSet<Result>?> LoadResults()
        {
            if (File.Exists(JSON_MALE_RESULTS) && File.Exists(JSON_FEMALE_RESULTS))
            {
                return Task.Run
                (() =>
                {
                    StreamReader reader = new StreamReader(
                        Settings.CupGender == true ?
                        JSON_MALE_RESULTS : JSON_FEMALE_RESULTS); //male = true, female = false

                    string results = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<HashSet<Result>>(results);

                });


            }
            else
            {
                return Task.Run
                (() =>
                {
                    RestClient restClient = new RestClient(
                        Settings.CupGender == true ?
                        WEB_MALE_RESULTS : WEB_FEMALE_RESULTS); //male = true, female = false

                    RestResponse restResponse = restClient.Execute<HashSet<Result>>(new RestRequest());
                    File.WriteAllText(Settings.CupGender == true ?
                        JSON_MALE_RESULTS : JSON_FEMALE_RESULTS, restResponse.Content.ToString()); //napravi lokalni file
                    return JsonConvert.DeserializeObject<HashSet<Result>>(restResponse.Content);
                });
            }
        }
    }
}
