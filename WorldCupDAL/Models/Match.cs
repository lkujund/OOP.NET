using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupDAL.Models
{
    public class Match
    {
        public string venue { get; set; }
        public string location { get; set; }
        public string status { get; set; }
        public string time { get; set; }
        public string fifa_id { get; set; }
        public Weather weather { get; set; }
        public string attendance { get; set; }
        public List<string> officials { get; set; }
        public string stage_name { get; set; }
        public string home_team_country { get; set; }
        public string away_team_country { get; set; }
        public DateTime datetime { get; set; }
        public string winner { get; set; }
        public string winner_code { get; set; }
        public Team home_team { get; set; }
        public Team away_team { get; set; }
        public List<Event> home_team_events { get; set; }
        public List<Event> away_team_events { get; set; }
        public TeamStatistics home_team_statistics { get; set; }
        public TeamStatistics away_team_statistics { get; set; }
        public DateTime last_event_update_at { get; set; }
        public object last_score_update_at { get; set; }
    }
}
