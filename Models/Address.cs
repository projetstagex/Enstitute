using System.ComponentModel.DataAnnotations;

namespace Enstitute
{
    public class Address
    {   

        [Key]
        public int ID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string StreetAdress { get; set; }

        [StringLength(100)]
        public string AppartementAdress { get; set; }
        
        [Required]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        public Address(){}
        public Address(string sa, string aa, string zip, string city){
            City = city;
            ZipCode = zip;
            AppartementAdress = aa;
            StreetAdress = sa;
        }
    }
}
