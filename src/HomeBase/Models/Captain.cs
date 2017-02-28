using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBase.Models
{
    public class Captain
    {
        public int CaptainID { get; set; }

        //[Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }   
        
        //[Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstName; }

        }
        [DisplayFormat(NullDisplayText = "No Team Assignment")]
        public Team Team { get; set; }

        //if we want him to have more then one team to captain?
        //public ICollection<Team> Teams { get; set; }
       




    }
}
