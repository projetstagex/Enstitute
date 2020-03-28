using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enstitute
{
    public class Formation
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int TotalYears { get; set; }

        [Required]
        public string Label { get; set; }

        [Required]
        public string Code { get; set; }

        [ForeignKey("Departement")]
        public int DepartementID { get; set; }
        public Departement Departement { get; set; }

        
        public Formation(){}

        public Formation(int totalyears, string label, string code, Departement departement){
            Label = label; 
            Code = code; 
            DepartementID = departement.ID;
            Departement = departement;
        }

    }
}
