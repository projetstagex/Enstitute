using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Enstitute
{
    public class Absence
    {
        [Key]
        public int ID { get; set; }
        
        [ForeignKey("Session")]
        public int SessionID {get;set;}
        public Session Session {get;set;}
        
        [ForeignKey("Student")]
        public string StudentID { get; set; }
        public Student Student { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}

        public Boolean Status {get;set;}

        public Absence(){}

        public Absence(Session session, Student student, DateTime date, Boolean status){
            Status = Status;
            SessionID = session.ID;
            Session = session;
            Student = student;
            StudentID = student.InscriptionNumber;
            Date = date;
        }
    }   
}
