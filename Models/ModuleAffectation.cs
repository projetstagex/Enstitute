using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enstitute
{
    public class ModuleAffectation
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Group")]
        public int GroupID { get; set; }
        public Group Group { get; set; }

        [ForeignKey("Module")]
        public int ModuleID { get; set; }
        public Module Module { get; set; }

        [ForeignKey("Former")]
        public string FormerID { get; set; }
        public Former Former { get; set; }

        public ModuleAffectation(){}
        public ModuleAffectation(Group g, Former f, Module m )
        {
            Group = g ;
            GroupID = g.ID;
            Former = f;
            FormerID = f.ID;
            Module = m;
            ModuleID = m.ID;
        }

    }
}
