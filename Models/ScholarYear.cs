using System.ComponentModel.DataAnnotations;



namespace Enstitute
{
    public class ScholarYear
    {
        [Key]
        public int ID {get;set;}

        [Required]
        public string Code { get; set; }

        [Required]
        public int From {get;set;}
        
        [Required]
        public int To {get;set;}

        public ScholarYear() {}

        public ScholarYear(int from, int to){
            From = from; 
            To = to; 
            Code = from + "/" + to ;
        }
   
    }
}
