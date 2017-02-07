using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeBase.Models;

namespace HomeBase.Models
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int TeamID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string TeamName { get; set; }

        public int? CaptainID { get; set; }

        public Captain Captain {get;set;} 

        public ICollection<Player> Players { get; set; }
        
        //need to look how this relates to the program 
        //public Department Department { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        //need to build the team assignemnts class
        //public ICollection<TeamAssignments> Assignments { get; set; }

        



    }
}
