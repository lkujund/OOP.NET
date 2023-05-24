using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupDAL.Models
{
    public class Weather
    {
        public string humidity { get; set; }
        public string temp_celsius { get; set; }
        public string temp_farenheit { get; set; }
        public string wind_speed { get; set; }
        public string description { get; set; }
    }
}
