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
using System.Collections.Generic;

namespace Enstitute.Controllers
{
    public class FormationTrackController : Controller
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
        private readonly ILogger<FormationTrackController> _logger;

        public FormationTrackController(ILogger<FormationTrackController> logger){_logger = logger;}
        
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

        public int Authenticate(){
            var UID = Request.Cookies["UID"];
            var SID = Request.Cookies["USID"];
            if ( UID != null && SID != null ){
                var db = new ApplicationContext();
                var sessionCount = db.UserSession.Where( us => us.UserID == UID && us.ID == SID && us.Deleted == false ).Count();
                // si la session existe
                if ( sessionCount != 0 ){
                    var user = db.User.Where( s => s.ID == UID ).First();
                    if ( user.is_actif && ( user.is_director || user.is_secretary || user.is_former ) ){
                        return 0;
                    }
                }
            }
            return -1;
        }

        public IActionResult Index(){
            if ( Authenticate() != 0 ) {
                return Redirect("Authentification");
            }
            var db = new ApplicationContext();
            var user_id = GetUserID();
            var user = db.User.Where( u => u.ID == user_id ).First();
            if ( user.is_director || user.is_secretary || user.is_staff ){
                ViewBag.UserName = HttpContext.Session.GetString("userName");
                ViewBag.acces_admin = "True"; 
                if ( user.is_director ){ViewBag.director = "True";}
                else {ViewBag.director = "False";}
                return View();
            }
            return Redirect("Authentification");
        }

        public async Task<List<ModuleAffectation>> LoadModulesAffectationsAsync ( string former_id ){
            var db = new ApplicationContext();
            return ( await db.ModuleAffectation
                    .Where( ma => ma.Former.ID == former_id )
                    .Include( ma => ma.Module )
                    .Include( ma => ma.Group )
                    .Include( ma => ma.Former )
                    .ToListAsync() );
        }

        public async Task<float> LoadPourcentageAsync ( int module_affectation_id ){
            var db = new ApplicationContext();
           
            var module_affectation = await db.ModuleAffectation
                .Where( ma => ma.ID == module_affectation_id )
                .Include( ma => ma.Former )
                .Include( ma => ma.Group )
                .Include( ma => ma.Module )
                .FirstAsync();
            
            var former = await db.Former
                .Where( f => f.ID == module_affectation.FormerID )
                .FirstAsync();

            var group = await db.Group 
                .Where( g => g.ID == module_affectation.GroupID )
                .FirstAsync();

            var module = await db.Module
                .Where( m => m.ID == module_affectation.ModuleID )
                .FirstAsync();
            
            var contexts_finished_count = await db.SessionContext
                .Where( sc => sc.Session.Schedule.Former.ID == former.ID )
                .Where( sc => sc.Context.Precision.ModuleID == module.ID )
                .Where( sc => sc.Session.GroupID == group.ID )
                .CountAsync();
            
            var contexts_count = await db.Context
                .Where( sc => sc.Precision.ModuleID == module.ID )
                .CountAsync();
            
           return ( (float)contexts_finished_count / (float)contexts_count) *100; 

        }

        public async Task<List<Former>> LoadFormersAsync ( ){
            var db = new ApplicationContext();
            return ( await db.Former.ToListAsync() );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(){return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });}
    }
}
