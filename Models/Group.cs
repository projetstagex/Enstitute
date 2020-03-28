using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enstitute
{
    public class Group
    {
      
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string Label { get; set; }

        [Required]
        public string Code { get; set; }

        [ForeignKey("Sector")]
        public int SectorID { get; set; }
        public  Sector  Sector { get; set; }

        [ForeignKey("ScholarYear")]
        public int ScholarYearID {get;set;}
        public ScholarYear ScholarYear {get;set;}

        public Group(){}

        public Group(string label,  Sector  sector, string code, ScholarYear scholarYear){
            Label = label;
            Code = code;
            SectorID = sector.ID;
            Sector = sector;
            ScholarYear = scholarYear;
            ScholarYearID = scholarYear.ID;
        }
    }
}
