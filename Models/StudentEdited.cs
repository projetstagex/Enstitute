using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enstitute
{
    public class StudentEdited  
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Student")]
        public string studentID { get; set; }
        public Student student { get; set; }

        [StringLength(50)]
        public string nationality { get; set; }

        public string firstName {get;set;}
                
        public string lastName {get;set;}

        [ForeignKey("Group")]
        public int groupID { get; set; }
        public Group group { get; set; }

        [EmailAddress]
        public string email { get; set; }

        [Phone]
        public string phone { get; set; }


        public StudentEdited(){}
        public StudentEdited ( string _first_name, string _last_name, 
            string _phone, string _email, Group _group, string _nationality, Student _student ) { 
            firstName = _first_name;
            lastName = _last_name;
            nationality = _nationality;
            phone = _phone;
            email = _email;
            group = _group;
            groupID = _group.ID;
            student = _student;
            studentID = _student.ID;
        }

    }
}
