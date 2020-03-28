using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Enstitute.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Enstitute.Controllers
{
    public class AuthentificationController : Controller
    {
        private readonly ILogger<AuthentificationController> _logger;

        public static string ComputeHash256( string data ){
            var foo = SHA256.Create();
            var hashValue = foo.ComputeHash( Encoding.UTF8.GetBytes(data)  )  ;
            var builder = new StringBuilder();
            for (int i = 0; i < hashValue.Length; i++)
            {
                builder.Append( hashValue[i].ToString("x2") );
            }
            return builder.ToString();
        } 

        public int Authenticate()
        {
            var UID = Request.Cookies["UID"];
            var SID = Request.Cookies["USID"];
            if ( UID != null && SID != null ){
                var db = new ApplicationContext();
                var sessionCount = db.UserSession.Where( us => us.UserID == UID && us.ID == SID && us.Deleted == false ).Count();
                // si la session existe
                if ( sessionCount != 0 )
                    return 0;
            }
            return -1;
        }
        public AuthentificationController(ILogger<AuthentificationController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            if ( Authenticate() == 0 ) 
                return Redirect("Home");
            return View();
        }

        [HttpPost]
        public async Task<Response> ValidateCredentialsAsync(string username, string password) {
            var db = new ApplicationContext();
            var encypted_password = ComputeHash256( password );
            try {
                var user = await db.User.Where( u => u.Username == username && u.Password == encypted_password && u.is_actif == true).FirstAsync();
                // si l'utilisateur n'existe pas 
                if ( user == null ){    
                    return new Response(1, "NotFound"); 
                }
                // s'il existe
                return new Response(0, "Done");
            }
            // ajout de try catch{} puisque ce putain de ASP lance une error lorcequ'il trouve rien en base de doonée à la place de mettre null
            catch {
                return new Response(1, "NotFound");
            }
           
        }

        [HttpPost]
        public async Task<Response> AuthenticateAsync(string username, string password) {
            var db = new ApplicationContext();  
            var encypted_password = ComputeHash256( password );
            // grab user from database
            var user = await db.User.Where( u => u.Username == username && u.Password == encypted_password ).FirstAsync();          
            var session = new UserSession();
            var id = "";
            // generate usersessionID
            while ( true ) {
                id = session.GenrateID();
                try {
                    var temp = db.UserSession.Where( us => us.ID == id).First() ;
                    if ( temp == null ) break;
                }
                catch {break;}
            }
            // create user session
            session.User = user;
            session.UserID = user.ID;
            session.LoginDate = DateTime.Now;
            session.ID = id;
            // update database
            db.Add(session);
            db.SaveChanges();
            // create cookie
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("USID", session.ID, option);
            Response.Cookies.Append("UID", user.ID, option);
            HttpContext.Session.SetString("userID", user.ID);
            HttpContext.Session.SetString("userName", user.Username);
            HttpContext.Session.SetString("fullName", user.GetFullName());
            // return redirect
            return new Response(0, "done");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
