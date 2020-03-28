using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Enstitute
{
    public class Sector
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Label { get; set; }

        [Required]
        public string Code {get;set;} 
        
        [ForeignKey("Grade")]
        public int GradeID { get; set; }
        public Grade Grade { get; set; }

        [ForeignKey("Formation")]
        public int FormationID { get; set; }
        public Formation Formation { get; set; }

        public Sector(){}

        public Sector(string label, string code, Grade gr, Formation formation){
            Label = label;
            Code = code;
            Grade = gr;
            GradeID = gr.ID;
            Formation = formation;
            FormationID = formation.ID;
        }
    }
}
