using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Enstitute
{
    public class UserSession
    {
        [Key]
        public string ID { get; set; }
        
        [ForeignKey("User")]
        public string UserID {get;set;}
        public User User {get;set;}

        [Required]
        public DateTime LoginDate {get;set;}

        public Boolean Deleted {get;set;}
        
        public string GenrateID() {
            StringBuilder builder = new StringBuilder();  
            Random random = new Random();  
            for (int i = 0; i < 15; i++)  {  
                builder.Append( Convert.ToChar( Convert.ToInt32( Math.Floor( 26 * random.NextDouble() + 65 ) ) ) );  
            }  
            return builder.ToString();  
        }

        public UserSession(){}

        public UserSession( DateTime datetime, User user ){
            User = user; 
            UserID = User.ID;
            LoginDate = datetime; 
            Deleted = false;
        }
    }
}
