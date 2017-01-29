using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBase.Models
{
    public class Player
    {
        public int PlayerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string Experience { get; set; }
        public string TeamRequested { get; set; }
        public DateTime EnrollmentDate { get; set;}
      
        public Team Team { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        

    }
}
