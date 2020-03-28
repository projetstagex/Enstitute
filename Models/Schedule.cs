using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Enstitute
{
    public class Schedule
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Former")]
        public string FormerID { get; set; }
        public Former Former { get; set; }

        [ForeignKey("ScholarYear")]
        public int ScholarYearID {get;set;}
        public ScholarYear ScholarYear {get;set;}

        [NotMapped]
        public List<Session> Sessions { get; set; }

        public Schedule(){}

        public Schedule( Former former, ScholarYear scholarYear ){
            FormerID = former.RegistrationNumber;
            ScholarYearID = scholarYear.ID;
            ScholarYear = scholarYear;
            Former = former;
        }
    }
}
