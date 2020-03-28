using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Enstitute.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Enstitute.Controllers
{


    public class FormationController : Controller
    {   

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
        
        private readonly ILogger<FormationController> _logger;

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

        public FormationController(ILogger<FormationController> logger)
        {
            _logger = logger;
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

        public IActionResult Index()
        {
            if ( Authenticate() == 0){
                ViewBag.UserName = HttpContext.Session.GetString("userName");
                var db = new ApplicationContext();
                var user_id = GetUserID();
                var user = db.User.Where( u => u.ID == user_id ).First();

                if ( user.is_director || user.is_secretary || user.is_staff ){
                    ViewBag.acces_admin = "True"; 
                }
                else{
                    return Redirect("Home");
                }

                if ( user.is_director ){
                ViewBag.director = "True";
                }

                else {
                    ViewBag.director = "False";
                }
                
                return View();
            }
            return Redirect("Authentification");
        }


        [HttpPost]
        public async Task<List<ModuleAffectation>> LoadModulesAffectationsAsync(){
            var db = new ApplicationContext();
            return ( 
                await db.ModuleAffectation
                .Include( ma => ma.Module )
                .Include( ma => ma.Former )
                .Include( ma => ma.Group )
                .ToListAsync()
            );
        }

        [HttpPost]
        public async Task<List<SessionContext>> LoadSessionsAsync( int module_affectation_id ){
            var db = new ApplicationContext();
            
            var module_affectation = await db.ModuleAffectation
                .Where( ma => ma.ID == module_affectation_id )
                .Include( ma => ma.Group )
                .Include( ma => ma.Former )
                .Include( ma => ma.Module )
                .FirstAsync();

            var former = module_affectation.Former.ID;
            var group = module_affectation.Group.ID;
            var module = module_affectation.Module.ID;

            var sessions = await db.SessionContext
                .Where( s => s.Session.Group.ID == group )
                .Where( s => s.Session.Schedule.Former.ID == former )
                .Where( s => s.Context.Precision.Module.ID == module )
                .Include( s => s.Session )
                .Include( s => s.Session.Day )
                .ToListAsync();
            
            return sessions;
        }



        [HttpPost]
        public async Task<List<Session>> LoadSessionsRattAsync(int module_affectation_id){
            var db = new ApplicationContext();
            var module_affectation = await db.ModuleAffectation
                .Where( ma => ma.ID == module_affectation_id )
                .Include( ma => ma.Group )
                .Include( ma => ma.Former )
                .Include( ma => ma.Module )
                .FirstAsync();
            
            var scholar_year = GetScholarYear();

            var schedule = await db.Schedule
                .Where( s => s.Former.ID == module_affectation.Former.ID )
                .Where( s => s.ScholarYearID == scholar_year.ID )
                .FirstAsync(); 

            var sessions = await db.Session
                .Where( s => s.is_catching == true )
                .Where( s => s.GroupID == module_affectation.GroupID )
                .Where( s => s.ScheduleID == schedule.ID )
                .Include( s => s.Day )
                .Include( s => s.Group )
                .ToListAsync();

            return sessions;
        }

        public ScholarYear GetScholarYear(){
            var db = new ApplicationContext();
            var year_now = DateTime.Now.Date.Year;
            var month_now = DateTime.Now.Date.Month;
            
            // Detect This scholar year algorithm
            if ( month_now < 8 ) {
                return (  
                    db.ScholarYear
                    .Where( s => s.To == year_now )
                    .First() 
                );
            }
            else {
                return ( 
                    db.ScholarYear
                    .Where( s => s.From == year_now )
                    .First() 
                );
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
