using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enstitute
{
    public class ActionGroup
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
        public int GroupEditedID {get;set;}
        public GroupEdited GroupEdited {get;set;}


        public ActionGroup(){}

        public ActionGroup( string description, DateTime dateTime, Secretary user, GroupEdited groupEdited ){
            Description = description; 
            DateTime = dateTime;
            Secretary = user;
            SecretaryID = user.ID;
            GroupEditedID = groupEdited.ID;
            GroupEdited = groupEdited;
        }
    }
}
