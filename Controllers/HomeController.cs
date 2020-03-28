using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Enstitute.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Enstitute.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public Response SignOut(){
            var SID = Request.Cookies["USID"];
            var db = new ApplicationContext();
            var userSession = db.UserSession.Where( us => us.ID == SID ).First();
            userSession.Deleted = true;
            db.UserSession.Update( userSession );
            db.SaveChanges();    
            Response.Cookies.Delete("UID");
            Response.Cookies.Delete("USID");
            return new Response(0, "done");
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
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string GetUserID(){
            // extract user ID *************************************
            var userID = "";
            try {
                userID = HttpContext.Session.GetString("userID");
            }
            catch {
                userID = Request.Cookies["UID"];
                HttpContext.Session.SetString("userID", userID);
            }
            // *****************************************************
            return userID;
        }

        public IActionResult Index()
        {
            if ( Authenticate() == 0 ) 
            {
                var db = new ApplicationContext();
                var user_id = GetUserID();
                var user = db.User.Where( u => u.ID == user_id ).First();
                if ( user.is_director || user.is_secretary || user.is_staff ){
                    ViewBag.acces_admin = "True"; 
                }
                else{
                    ViewBag.acces_admin = "False";
                }
                ViewBag.UserName = HttpContext.Session.GetString("userName");
                if ( user.is_director ){ViewBag.director = "True";}
                else {ViewBag.director = "False";}
                return View();
            }    
            return Redirect("Authentification");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
