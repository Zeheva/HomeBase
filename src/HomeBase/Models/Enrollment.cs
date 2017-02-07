using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBase.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        public int PlayerID { get; set; }
        public Player Player { get; set; }

        //single team (so team ID FK) Team Team (Navigaion)
        public int TeamID { get; set; }
        public Team Team { get; set; }

        [DisplayFormat(NullDisplayText = "No Position")]
        public Position? Position { get; set; }
        //changed from Player based on Player controller action method Details for .include
        
      
       

  
    }
    //do i need this if the team is its own table?
    public enum Position
    {
        Pitcher, Catcher, FirstBaseman, SecondBaseman, 
        ThirdBaseman, ShortStop, LeftFielder, CenterFielder,
        RightFielder, WaterBoy
    }

 
}

