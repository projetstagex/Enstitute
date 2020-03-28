using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enstitute
{
    public class Module
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Code {get;set;}
        
        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Required]
        public int Multiplier { get; set; }

        public bool Approved {get; set; }

        public Module(){}
        public Module( int mul, string title, string code ){
            Title = title;
            Multiplier = mul;
            Code = code;
        }

        [NotMapped]
        public List<Absence> Absences {get;set;}

        [NotMapped]
        public int AbsencesCount {get;set;}

        [NotMapped]
        public List<Precision> Precisions {get;set;}
    }
}
