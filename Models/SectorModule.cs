using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enstitute
{
    public class SectorModule
    {
        [Key]
        public int ID { get; set; }

        
        [ForeignKey("Sector")]
        public int SectorID { get; set; }
        public Sector Sector { get; set; }

        [ForeignKey("Module")]
        public int ModuleID { get; set; }
        public Module Module { get; set; }

        public SectorModule(){}

        public SectorModule( Sector sector, Module module){
            Sector = sector;
            SectorID = sector.ID;
            Module = module;
            ModuleID = module.ID;
        }
    }
}
