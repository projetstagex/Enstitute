using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enstitute
{
    public class ActionStudent
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public DateTime DateTime {get;set;}

        [Required]
        public string Description {get;set;}


        [Required]
        [ForeignKey("Secretary")]
        public string SecretaryID {get;set;}
        
        [Required]
        public Secretary Secretary {get;set;}


        [Required]        
        [ForeignKey("Student")]
        public int StudentEditID {get;set;}
        
        [Required]
        public StudentEdited StudentEdited {get;set;}


        public ActionStudent(){}

        public ActionStudent( string description, DateTime dateTime, Secretary user, StudentEdited student ){
            Description = description; 
            DateTime = dateTime;
            Secretary = user;
            SecretaryID = user.ID;
            StudentEditID = student.id;
            StudentEdited = student;
        }
        
    }
}
