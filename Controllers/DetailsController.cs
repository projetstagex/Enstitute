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


    public class DetailsController : Controller
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
        private readonly ILogger<DetailsController> _logger;

        public DetailsController(ILogger<DetailsController> logger)
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

        public async Task<IActionResult> Index(string SID){
            // Authenticate ******************************************************
            if ( Authenticate() != 0)
                return Redirect("Authentification");
            // *******************************************************************
            
            // initiliase dabase context and student variable ********************
            var db = new ApplicationContext();
            Student student;
            // *******************************************************************

            // get student from database  ****************************************
            try {
                student = await db.Student
                    .Where( s => s.InscriptionNumber == SID )
                    .Include(s => s.Address)
                    .Include( s => s.Gender)
                    .Include( s => s.Group )
                    .FirstAsync();

                if (student == null) 
                    return Redirect("Home");
            }
            // i launched try catch because this fucking framwork is a bullshit 
            catch {
                return Redirect("Home");
            }
            // ********************************************************************

            ViewBag.Student = student;

            // extract user ID *************************************
            var userID = GetUserID();
            // *****************************************************
            
            // get the user from the database *************************************
            var user = await db.User
                .Where( u => u.ID == userID )
                .FirstAsync();
            // ********************************************************************
            
            // si l'utilisateur est un directeur ou secretaire  *******************
            // retournez tous les grades dans la select ***************************
            if ( user.is_director || user.is_secretary ){
                // extract all modules where student is subscribed ****************
                var modules = await db.ModuleAffectation
                    .Where( mf => mf.GroupID == student.GroupID )
                    .Include( mf => mf.Module)
                    .Include( mf => mf.Former )
                    .ToListAsync();
                // ****************************************************************
                var i = 0;
                foreach (var m in modules){
                    
                    var absences =  db.Absence
                        .Where ( a => a.Student.ID == student.ID )
                        .ToList();
                    var abs = new List<Absence>();
                    foreach (var absence in absences)
                    {
                        var session = await db.Session
                            .Where( s => s.ID == absence.SessionID )
                            .FirstAsync();
                        var group = await db.Group
                            .Where ( g => g.ID == student.GroupID )
                            .FirstAsync();
                        var schedule = await db.Schedule
                            .Where ( sc => sc.ID == session.ScheduleID )
                            .FirstAsync();
                        var former = await db.Former
                            .Where( f => f.ID == schedule.Former.ID )
                            .FirstAsync();
                        var moduleAffectation = await db.ModuleAffectation
                            .Where ( ma => ma.Former.ID == former.ID 
                                && ma.Group.ID == group.ID )
                            .FirstAsync();
                        var module = moduleAffectation.Module;
                        if (module.ID == m.Module.ID) {
                            i += 1;
                            m.Module.AbsencesCount += 1;
                            abs.Append(absence);
                        }
                    }
                    m.Module.Absences = abs;
                    i = 0;
                }
                ViewBag.Modules = modules;
                ViewBag.Modulesjs = JsonConvert.SerializeObject(modules);
            
                // Total absences per student *************************************
                var total_absences = await db.Absence
                    .Where ( a => a.Student.ID == student.ID )
                    .CountAsync();
                var total_delays = await db.Delay
                    .Where ( a => a.Student.ID == student.ID )
                    .CountAsync();
                ViewBag.TotalDelays = total_delays;
                ViewBag.TotalAbsences = total_absences;
                // ****************************************************************
            }
            // ********************************************************************

            // SI L'UTILISATEUR EST UN FORMATEUR **********************************
            else if ( user.is_former ){
                // get formateur de la BD
                var former = await db.Former
                    .Where( f => f.ID == user.ID )
                    .FirstAsync();
                // **************************************************
                // extract all modules where student is subscribed **************** 
                var modules = await db.ModuleAffectation
                    .Where( mf => mf.GroupID == student.GroupID )
                    .Where( mf => mf.FormerID == former.ID )
                    .Include( mf => mf.Module)
                    .Include( mf => mf.Former )
                    .ToListAsync();
                // ****************************************************************
                foreach (var m in modules){ 
                    var absences = await db.Absence
                        .Where ( a => a.Student.ID == student.ID )
                        .Where( a => a.Session.Schedule.Former.ID == former.ID )
                        .ToListAsync();
                    foreach (var absence in absences)
                    {
                        var session = await db.Session
                            .Where( s => s.ID == absence.SessionID )
                            .FirstAsync();
                        var group = await db.Group
                            .Where ( g => g.ID == student.GroupID )
                            .FirstAsync();
                        var moduleAffectation = await db.ModuleAffectation
                            .Where ( ma => ma.Former.ID == former.ID 
                            && ma.Group.ID == group.ID )
                            .FirstAsync();
                        var module = moduleAffectation.Module;
                        if (module.ID == m.Module.ID) 
                            m.Module.AbsencesCount += 1;

                    }
                }
                ViewBag.Modules = modules;
            
                // Total absences per student *************************************
                var total_absences = await db.Absence
                    .Where ( a => a.Student.ID == student.ID )
                    .CountAsync();
                var total_delays = await db.Delay
                    .Where ( a => a.Student.ID == student.ID )
                    .CountAsync();
                ViewBag.TotalDelays = total_delays;
                ViewBag.TotalAbsences = total_absences;
            }
            // ********************************************************************
            ViewBag.UserName = HttpContext.Session.GetString("userName");
            if ( user.is_director || user.is_secretary || user.is_staff ){
                ViewBag.acces_admin = "True"; 
            }
            else{
                ViewBag.acces_admin = "False";
            }

            if ( user.is_director ){
                ViewBag.director = "True";
            }
            else {
                ViewBag.director = "False";
            }
            return View();

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

