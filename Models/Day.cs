using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Enstitute
{
    public class Day
    {
        
        public int ID { get; set; }
        
        [Required]
        [StringLength(15)]
        public string Label { get; set; }

        public Day(){}

        public Day(String label, int id){
            Label = label;
            ID = id;
        }

        [NotMapped]
        public List<Session> Sessions { get; set; }
    }
}
