using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBase.Models
{
    public class TeamAssignment
    {
        public int CaptainID { get; set; }
        public int TeamID { get; set; }
        public Captain Captain { get; set; }
        public Team Team { get; set; }
    }
}
