using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace Enstitute
{
    public class Precision
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string Code {get;set;}

        [ForeignKey("Module")]
        public int ModuleID {get;set;}
        public Module Module {get;set;}

        [Required]
        [StringLength(15)]
        public string Label { get; set; }

        public Precision(){}
        public Precision(String label, string code, Module module){
            Label = label;
            Code = code;
            Module = module;
            ModuleID = module.ID;
        }

        [NotMapped]
        public List<Context> Contexts { get; set; }
    }
}
