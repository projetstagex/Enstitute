using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Enstitute
{
    public class Context
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Code {get;set;}
        
        [Required]
        [StringLength(15)]
        public string Label { get; set; }

        [Required]
        [ForeignKey("Precision")]
        public int PrecisionID {get;set;}
        public Precision Precision {get;set;}

        public Context(){}
        public Context(String label, Precision precision, string code){
            Label = label;
            Code = code;
            Precision = precision;
            PrecisionID = precision.ID;
        }

    }
}
