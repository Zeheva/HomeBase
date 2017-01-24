using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBase.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int PlayerID { get; set; }
        public int TeamID { get; set; }
        public Position? Position { get; set; }
        //changed from Player based on Player controller action method Details for .include
        public Player Player { get; set; }
        public Team Team { get; set; }
       

  
    }
    //do i need this if the team is its own table?
    public enum Position
    {
        Pitcher, Catcher, FirstBaseman, SecondBaseman, 
        ThirdBaseman, ShrotStop, LeftFielder, CenterFielder,
        RightFielder, WaterBoy
    }

 
}

