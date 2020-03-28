using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enstitute
{
    public class GroupEdited  
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Group")]
        public int GroupID { get; set; }
        public Group Group { get; set; }

        public string Code { get; set; }

        public string Label { get; set; }

        public GroupEdited(){}
        public GroupEdited ( string code, string label, Group group ) { 
           
            Code = code;
            Group = group;
            GroupID = group.ID;
            Label = label;
        }

    }
}
