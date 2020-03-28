using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Enstitute.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using Newtonsoft.Json;

namespace Enstitute.Controllers
{


    public class ProfileController : Controller
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
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger)
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

        public async Task<IActionResult> Index(){
            // Authenticate ******************************************************
            if ( Authenticate() != 0)
                return Redirect("Authentification");
            // *******************************************************************
            
            // initiliase dabase context and student variable ********************
            var db = new ApplicationContext();
            User user;
            // *******************************************************************

            // get student from database  *****************************************
            try {
                user = await db.User
                    .Where( s => s.ID == GetUserID() )
                    .Include(s => s.Address)
                    .Include( s => s.Gender)
                    .FirstAsync();

                if (user == null) 
                    return Redirect("Home");
            }
            // i launched try catch because this fucking framwork is a bullshit 
            catch {
                return Redirect("Home");
            }
            // ********************************************************************

                
            if ( user.is_former ){
                ViewBag.UserName = HttpContext.Session.GetString("userName");
                ViewBag.User = user;
                ViewBag.acces_admin = "False";
                ViewBag.Modules  = await db.ModuleAffectation
                    .Where( ma => ma.Former.ID == user.ID )
                    .Include( ma => ma.Module )
                    .Include( ma => ma.Group )
                    .ToListAsync();
                ViewBag.director = "False";
                return View();
            }
            else {
                return Redirect("Home");
            }

            // ***********************************************************
        }

        [HttpPost]
        public async Task<List<Absence>> LoadAbsencesAsync(int module_id, string student_id) {
            // Initialise Database ***********************************************
            var db = new ApplicationContext();

            var student = await db.Student
                .Where( s => s.InscriptionNumber == student_id )
                .FirstAsync();
    
            var module = await db.Module
                .Where( m => m.ID == module_id )
                .FirstAsync();

            var group = await db.Group
                .Where ( g => g.ID == student.GroupID )
                .FirstAsync();

            var abs = new List<Absence>();

            var absences = await db.Absence
                .Where( a => a.Student.ID == student.ID )
                .ToListAsync();

            int j = 0;
            for (int i = 0; i < absences.Count; i++)
            {
                var session = await db.Session
                    .Where( s => s.ID == absences[i].SessionID )
                    .FirstAsync();
                var schedule =  await db.Schedule
                    .Where ( sc => sc.ID == session.ScheduleID )
                    .FirstAsync();
                var former = await db.Former
                    .Where( f => f.ID == schedule.FormerID )
                    .FirstAsync();
                var moduleAffectation = await db.ModuleAffectation
                    .Where ( ma => ma.Former.ID == former.ID 
                        && ma.Group.ID == group.ID )
                    .Include( ma => ma.Module )
                    .FirstAsync();
                var m = moduleAffectation.Module;

                if (module.ID == m.ID)
                {
                    abs.Insert(j, absences[i]);
                    j += 1;
                }
            }
            return abs;
            //**********************************************************************
        }

        public async Task<Response> EditAbsenceAsync( int absence_id, string status ) {

            var db = new ApplicationContext();
            var absence = await db.Absence.Where( a => a.ID == absence_id ).FirstAsync();
            var status_bin = status == "False" ? false : true;
            absence.Status = status_bin;
            db.Absence.Update( absence );
            await db.SaveChangesAsync();
            return new Response( 0, "done" );

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

