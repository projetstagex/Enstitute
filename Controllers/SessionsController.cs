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


    public class SessionsController : Controller
    {   
        
        private readonly ILogger<SessionsController> _logger;

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

        public SessionsController(ILogger<SessionsController> logger)
        {
            _logger = logger;
        }

        public int Authenticate(){
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
            // Authenticate **************************************************
            if ( Authenticate() != 0)
                return Redirect("Authentification");
            // ***************************************************************

            // initialise DB and days list
            var db = new ApplicationContext();
            ViewBag.Days = db.Day.ToList();
            // ***************************************************************
            
            // extract user ID ***********************************************
            var userID = GetUserID();
            // ***************************************************************
            
            // get the user from the database ********************************
            var user = db.User.Where( u => u.ID == userID ).First();
            //****************************************************************

            // si l'utilisateur est un directeur ou secretaire ***************
            if (user.is_actif && ( user.is_director || user.is_secretary ) ){
                ViewBag.IsFormer = "false";
                ViewBag.Former = "";
                ViewBag.ID = "";
            }
            // ***************************************************************

            // si l'utilisateur est un FORMATEUR *****************************
            else if ( user.is_actif && user.is_former ) {
                // get formateur de la BD ************************************
                var former = db.Former.Where( f => f.ID == user.ID ).First();
                // ***********************************************************
                    
                ViewBag.IsFormer = "true";
                ViewBag.Former = former;
                ViewBag.ID = former.ID;
            }
                
            // ***************************************************************
            ViewBag.UserName = HttpContext.Session.GetString("userName");
            if ( user.is_director || user.is_secretary || user.is_staff ){
                    ViewBag.acces_admin = "True"; 
            }
            else{
                ViewBag.acces_admin = "False";
            }

            if ( user.is_director ){ViewBag.director = "True";}
            else {ViewBag.director = "False";}

            return View();
        }


        [HttpPost]
        public async Task<List<Former>> LoadFormersAsync(){
            var db = new ApplicationContext();
            return await db.Former.ToListAsync();
        }

        [HttpPost]
        public async Task<List<Session>> LoadSessionsAsync(string formerID){
            var db = new ApplicationContext();
            return await db.Session
                .Where( s => s.Schedule.FormerID == formerID && s.Schedule.ScholarYear.To == DateTime.Now.Year )
                .Include( s => s.Group.Sector.Grade )
                .Include( s=> s.Day )
                .ToListAsync();
        }

        [HttpPost]
        public async Task<List<Day>> LoadDaysAsync(){
            var db = new ApplicationContext();
            return await db.Day.ToListAsync();
        }

        [HttpPost]
        public async Task<List<Group>> LoadGroupsAsync(string formerID){
            // initialise DB and formations list
            var db = new ApplicationContext();
            // *****************************************************
            var groups =    from g in await db.Group.ToListAsync()
                            join ma in await db.ModuleAffectation.ToListAsync() on g.ID equals ma.GroupID
                            where ma.FormerID == formerID
                            select g;
            return groups.Distinct().ToList();
        }

        [HttpPost]
        public async Task<List<Module>> LoadModulesAsync( string formerID, int sessionID ){
            // initialise DB and formations list
            var db = new ApplicationContext();
            // GET THE GROUP **************************************
            var groupOfSession =  ( from g in await db.Group.ToListAsync()
                                    join s in await db.Session.ToListAsync() on g.ID equals s.GroupID 
                                    where s.ID == sessionID
                                    select g ).First()  ;    
            // *****************************************************
            
            // GET MODULES THE GROUP AND THE FORMER 
            var modules =   (from m in await db.Module.ToListAsync()
                            join ma in await db.ModuleAffectation.ToListAsync() on m.ID equals ma.ModuleID
                            where ma.FormerID == formerID && ma.GroupID == groupOfSession.ID 
                            select m).ToList();
            // *****************************************************

            // BOUCLE OVER MODULES 
            
            for (int i = 0; i < modules.Count; i++)
            {
                var precisions = await db.Precision.Where ( p => p.ModuleID == modules[i].ID ).ToListAsync();
                for (int j = 0; j < precisions.Count; j++)
                {
                    var contexts = await db.Context.Where( c => c.PrecisionID == precisions[j].ID ).ToListAsync();
                    precisions[j].Contexts = contexts;
                }
                modules[i].Precisions = precisions;
            }
            // *****************************************************
            return modules.ToList();
        }

        [HttpPost]
        public async Task<List<Module>> LoadModulesAddAsync( string formerID ){
            // initialise DB and formations list
            var db = new ApplicationContext();
            
            var modules =   from module in await db.Module.ToListAsync() 
                            join ma in await db.ModuleAffectation.ToListAsync() on module.ID equals ma.ModuleID 
                            where ma.FormerID == formerID
                            select module;
            return modules. Distinct() .ToList();
        }

        [HttpPost]
        public async Task<Response> AddSessionAsync( string formerID, int groupID, int dayID, int fh, int fm, int th, int tm, int time_part){
            // initialise DB and formations list
            var db = new ApplicationContext();
            // *****************************************************

            // check sessions time conflicts ***********************

            // *****************************************************

            // initialise prerequies *******************************
            var group = await db.Group.Where( g => g.ID == groupID ).FirstAsync();
            var former = await db.Former.Where( f => f.ID == formerID ).FirstAsync();
            var Schedule = await db.Schedule.Where( s => s.ScholarYear.To == DateTime.Now.Year && s.Former == former ).FirstAsync();
            var day = await db.Day.Where( d => d.ID == dayID ).FirstAsync();
            // ******************************************************

            // Add session to database ******************************
            var session = new Session(group,day, Schedule, fh, fm, th, tm, time_part, false );
            await db.AddAsync(session);
            await db.SaveChangesAsync();
            // ******************************************************
            
            // return ***********************************************
            return new Response(0, "done");
            // ******************************************************
        }

        [HttpPost]
        public async Task<Response> DeleteSessionAsync(int sessionID){
            // initialise DB and formations list
            var db = new ApplicationContext();
            // *****************************************************

            var session = await db.Session.Where( s => s.ID == sessionID ).FirstAsync();
            db.Session.Remove( session );
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }


        [HttpPost]
        public async Task<Response> AddSessionContextAsync(int sessionID, int contextID, int year, int month, int day){
            // initialise DB and formations list
            var db = new ApplicationContext();
            // *****************************************************

            var session = await db.Session.Where( s => s.ID == sessionID ).FirstAsync();
            var context = await db.Context.Where( c => c.ID == contextID ).FirstAsync();
            var date = new DateTime(year, month, day).Date;
            var sessionContext = new SessionContext(context, session, date);
            await db.SessionContext.AddAsync( sessionContext );
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
