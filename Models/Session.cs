using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enstitute
{
    public class Session
    {
        [Key]
        public int ID { get; set; } 
        
        [Required]
        public Boolean is_catching {get;set;}

        [Required]
        [Range(0, 18)]
        public int FromHour { get; set; }

        [Required]
        [Range(0, 59)]
        public int FromMinute {get; set;}

        [Required]
        [Range(0, 20)]
        public int ToHour { get; set; }
        
        [Required]
        [Range(0, 59)]
        public int ToMinute {get; set;}

        [Required]
        [Range(1, 5)]
        public int TimePart {get;set;}

        [ForeignKey("Schedule")]
        public int ScheduleID { get; set; }
        public Schedule Schedule { get; set; }

        [ForeignKey("Day")]
        public int DayID { get; set; }
        public Day Day { get; set; }

        [ForeignKey("Group")]
        public int GroupID { get; set; }
        public Group Group { get; set; }


        public int GetTimeStamp()
        {
            return  (ToHour * 60) + ToMinute - (FromHour * 60) - FromMinute; 
        }

        public Session(){}

        public Session(Group group, Day d, Schedule s, int fh, int fm, int th, int tm, int time_part, bool catching ){
            Day = d;
            Group = group;
            GroupID = Group.ID;
            DayID = d.ID;
            Schedule = s;
            ScheduleID = s.ID;
            FromHour = fh;
            FromMinute = fm;
            ToHour = th;
            ToMinute = tm; 
            is_catching = catching;
            TimePart = time_part;
        }
    }
}
