using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Enstitute.Controllers
{
    public class Response {   

        public Response(){}
        
        public Response(int code, string message){
            this.code = code;
            this.message = message;
        }

        public int code {get;set;}

        public string message{get;set;}
    }

    public class Base {
        public string ComputeHash256( string data ){
            var foo = SHA256.Create();
            var hashValue = foo.ComputeHash( Encoding.UTF8.GetBytes(data)  )  ;
            var builder = new StringBuilder();
            for (int i = 0; i < hashValue.Length; i++)
            {
                builder.Append( hashValue[i].ToString("x2") );
            }
            return builder.ToString();
        } 
        public Student GetStudent( string firstname, string lastname ){
            var db = new ApplicationContext();
             var student = db.Student
                .Where( s => s.FirstName == firstname && s.LastName == lastname )
                .FirstOrDefault();

            if ( student == null ) {  
                var _student = db.Student
                    .Where( s => s.LastName == lastname )
                    .FirstOrDefault();

                if ( student == null ) {
                }
                return _student;
            }
            return student;
        }
    }

    // public class LoadGroups
    // {
    //     public List<Group> Groups {get;set;}
    // } 
    
}