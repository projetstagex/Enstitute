using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enstitute
{
    public class Delay
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int Duration { get; set; }

        [ForeignKey("Student")]
        public string StudentID { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Session")]
        public int SessionID { get; set; }
        public Session Session { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}

        public Delay(){}

        public Delay(Session session, Student student, DateTime date, int duration){
            SessionID = session.ID;
            Session = session;
            Student = student;
            StudentID = student.InscriptionNumber;
            Date = date;
            Duration = duration;
        }
    }
}
