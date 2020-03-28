using System.ComponentModel.DataAnnotations;


namespace Enstitute
{
    public class Gender
    {
        [Key]
        public int ID { get; set; }

        public string Label {get;set;}
        public int Code {get;set;}

        public Gender() {}

        public Gender(string label, int code){
            Label = label;
            Code = code;
        }
   
    }
}
