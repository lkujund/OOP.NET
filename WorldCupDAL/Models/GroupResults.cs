using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupDAL.Models
{
    public class GroupResults
    {
        public int id { get; set; }
        public string letter { get; set; }
        public List<Result> ordered_teams { get; set; }
    }
}
