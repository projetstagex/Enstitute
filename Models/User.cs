using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Enstitute
{
    public abstract class User 
    {
        [Key]
        public string ID { get; set; }

        [Required]
        [StringLength(15)] 
        public string NIC { get; set; }

        [Required]
        public string Username {get;set;}
                
        [Required]
        public string FirstName {get;set;}
                
        [Required]
        public string LastName {get;set;}
                
        [Required]
        public string Password {get; set;}
                
        [Required]
        public Boolean is_staff {get;set;}
                
        [Required]
        public Boolean is_director {get;set;}
                
        [Required]
        public Boolean is_former {get;set;}
                
        [Required]
        public Boolean is_secretary {get;set;}
        
        [Required]
        public Boolean is_actif {get;set;}
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }


        [ForeignKey("Address")]
        public int AddressID { get; set; }
        public Address Address { get; set; }

        [ForeignKey("Gender")]
        public int GenderID { get; set; }
        public Gender Gender { get; set; }

     

        public User(){}

       

        public void SetPassword(string password) {
            var foo = SHA256.Create();
            var hashValue = foo.ComputeHash( Encoding.UTF8.GetBytes(password)  )  ;
            var temp = new StringBuilder();
            for (int i = 0; i < hashValue.Length; i++){temp.Append( hashValue[i].ToString("x2") );}
            Password =  temp.ToString();    
        }
        public string GenrateID(int length = 15) {
            StringBuilder builder = new StringBuilder();  
            Random random = new Random();  
            for (int i = 0; i < length; i++)  {  builder.Append( Convert.ToChar( Convert.ToInt32( Math.Floor( 26 * random.NextDouble() + 65 ) ) ) );  }  
            return builder.ToString();  
        }

        public string GetFullName() { return LastName + " " + FirstName;}
        public int GetAge()
        {
            DateTime now = DateTime.Now.Date;
            int age = now.Year - BirthDate.Year;
            if (now.Month < BirthDate.Month || (now.Month == BirthDate.Month && now.Day < BirthDate.Day))
                age--;
            return age;
        }
    }
}
