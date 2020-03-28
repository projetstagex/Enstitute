using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Enstitute
{
    public class Grade
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string Label { get; set; }

         
        [Required]
        public string Code { get; set; }
        
        public Grade(){}

        public Grade(string label, string code) {
            Label = label;
            Code = code;
        }
    }
}
