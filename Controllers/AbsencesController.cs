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


    public class AbsencesController : Controller
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
        
        private readonly ILogger<AbsencesController> _logger;

        public AbsencesController(ILogger<AbsencesController> logger)
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
                if ( sessionCount != 0 ){
                    var user = db.User.Where( s => s.ID == UID ).First();
                    if ( user.is_actif && ( user.is_director || user.is_secretary || user.is_former ) ){
                        return 0;
                    }
                }
            }
            return -1;
        }

        public IActionResult Check(){
            if ( Authenticate() == 0){
                ViewBag.UserName = HttpContext.Session.GetString("userName");
                var db = new ApplicationContext();
                var user_id = GetUserID();
                var user = db.User.Where( u => u.ID == user_id ).First();
                if ( user.is_director || user.is_secretary || user.is_staff ){
                    ViewBag.acces_admin = "True"; 
                }
                else{
                    ViewBag.acces_admin = "False";
                }
                
                return View();
            }
            return Redirect("Authentification");
        }
        
        public IActionResult Index(){
            if ( Authenticate() == 0){
                ViewBag.UserName = HttpContext.Session.GetString("userName");
                var db = new ApplicationContext();
                var user_id = GetUserID();
                var user = db.User.Where( u => u.ID == user_id ).First();
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

            return Redirect("Authentification");
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

        [HttpPost]
        public async Task<List<Formation>> LoadFormationsAsync(){
            
            // initialise DB and formations list
            var db = new ApplicationContext();
            var formations = new List<Formation>();
            // *****************************************************

            // extract user ID *************************************
            var userID = GetUserID();
            // *****************************************************
            
            // get the user from the database **********************
            var user = await db.User.Where( u => u.ID == userID ).FirstAsync();
            //******************************************************
            
            // si l'utilisateur est un directeur ou secretaire 
            // retournez tous les formations dans la select
            if  ( user.is_director || user.is_secretary ) {
                formations = await db.Formation.ToListAsync();
            }
            //******************************************************
            
            //  si l'utilisateur est un formateur 
            // retorunez just les formations dont il a des groupes 
            else if ( user.is_former){
                
                // get formateur de la BD
                var former = await db.Former.Where( f => f.ID == user.ID ).FirstAsync();
                // **************************************************

                // get les formations spécifique du formateurs
                var foo =   from formation in db.Formation.ToList()
                            join s in db.Sector.ToList() on formation.ID equals s.FormationID
                            join g in db.Group.ToList() on s.ID equals g.SectorID 
                            join ma in db.ModuleAffectation.ToList() on g.ID equals ma.GroupID 
                            where ma.FormerID == former.ID
                            select formation;
                formations = foo.Distinct().ToList();
                // **************************************************
            }
            //******************************************************
            return formations;
        }
        
        [HttpPost]
        public async Task<List<Grade>> LoadGradesAsync(){
            
            // initialise DB and grades list
            var db = new ApplicationContext();
            var grades = new List<Grade>();
            // *****************************************************

            // extract user ID *************************************
            var userID = GetUserID();
            // *****************************************************
            
            // get the user from the database **********************
            var user = await db.User.Where( u => u.ID == userID ).FirstAsync();
            //******************************************************
            
            // si l'utilisateur est un directeur ou secretaire 
            // retournez tous les grades dans la select
            if ( user.is_director || user.is_secretary ) {
                grades = await db.Grade.ToListAsync();
            }
            //******************************************************
            
            //  si l'utilisateur est un formateur 
            // retorunez just les grades dont il a des groupes 
            else if ( user.is_former){
                
                // get formateur de la BD
                var former = await db.Former.Where( f => f.ID == user.ID ).FirstAsync();
                // **************************************************

                // get les grades spécifique du formateurs
                var foo =   from grade in db.Grade.ToList()
                            join s in db.Sector.ToList() on grade.ID equals s.GradeID
                            join g in db.Group.ToList() on s.ID equals g.SectorID 
                            join ma in db.ModuleAffectation.ToList() on g.ID equals ma.GroupID
                            where ma.FormerID == former.ID
                            select grade;  

                grades = foo.Distinct().ToList();
                // **************************************************
            }
            //******************************************************
            return grades;
        }
        
        [HttpPost]
        public async Task<List<Group>> LoadGroupsAsync(int gradeId, int formationID) {
            
            // initialise DB and grades list
            var db = new ApplicationContext();
            var groups = new List<Group>();
            // *****************************************************

            // extract user ID *************************************
            var userID = GetUserID();
            // *****************************************************
            
            // get the user from the database **********************
            var user = await db.User.Where( u => u.ID == userID ).FirstAsync();
            //******************************************************
            
            // si l'utilisateur est un directeur ou secretaire 
            // retournez tous les grades dans la select
            var scholar_year = GetScholarYear();

            if ( user.is_director || user.is_secretary ) {
                
                var foo =   from g in await db.Group.ToListAsync()
                            join sy in await db.ScholarYear.ToListAsync()
                                on g.ScholarYearID equals sy.ID
                            join s in await db.Sector.ToListAsync() 
                                on g.SectorID equals s.ID
                            where  s.FormationID == formationID 
                                && s.GradeID == gradeId 
                                && sy.ID == scholar_year.ID
                            select g;
                
                groups = foo.Distinct().ToList();
            }
            //******************************************************
            
            //  si l'utilisateur est un formateur 
            // retorunez just les grades dont il a des groupes 
            else if ( user.is_former){
                
                // get formateur de la BD
                var former = await db.Former.Where( f => f.ID == user.ID ).FirstAsync();
                // **************************************************

                // get les grades spécifique du formateurs
                var foo =   from g in await db.Group.ToListAsync()
                            join sy in await db.ScholarYear.ToListAsync()
                                on g.ScholarYearID equals sy.ID
                            join s in await db.Sector.ToListAsync() 
                                on g.SectorID equals s.ID
                            join ma in db.ModuleAffectation.ToList() on g.ID equals ma.GroupID
                            where  s.FormationID == formationID 
                                && s.GradeID == gradeId 
                                && ma.FormerID == former.ID 
                                && sy.ID == scholar_year.ID
                            select g;

                groups = foo.Distinct().ToList();
                // **************************************************
            }
            //******************************************************
            return groups;
        }
        
        [HttpPost]
        public async Task<List<Student>> LoadStudentsAsync(int group_id) {
            // Initialise Database ***********************************************
            var db = new ApplicationContext();
            // *******************************************************************

            // return List of students that are on the group in the parameters ***
            return await db.Student
                .Where( g => g.GroupID == group_id )
                .Include( g => g.Gender )
                .Include( g => g.Address )
                .Where( g => g.Is_training == true )
                .ToListAsync();
            //**********************************************************************
        }

        [HttpPost]
        public async Task<List<Session>> LoadSessionsAsync( int dayID, int groupID ){
            // initialise DB and sessions list
            var db = new ApplicationContext();
            var sessions = new List<Session>();
            // *****************************************************

            // extract user ID *************************************
            var userID = GetUserID();
            // *****************************************************
            
            // get the user from the database **********************
            var user = await db.User.Where( u => u.ID == userID ).FirstAsync();
            //******************************************************
            
            // si l'utilisateur est un directeur ou secretaire 
            // retournez tous les sessions dans la select
            if ( user.is_director || user.is_secretary ) {
                sessions = await db.Session
                    .Where ( s => s.GroupID == groupID && s.DayID == dayID)
                    .Include( s => s.Day )
                    .ToListAsync();
            }
            //  si l'utilisateur est un formateur 
            // retorunez just les grades dont il a des groupes 
            else if (user.is_former){
                
                // get formateur de la BD *************************
                var former = await db.Former.Where( f => f.ID == user.ID ).FirstAsync();
                // ************************************************
                sessions = await db.Session
                    .Where ( s => s.GroupID == groupID && s.DayID == dayID && s.Schedule.ScholarYear.To == DateTime.Now.Year && s.Schedule.Former == former)
                    .ToListAsync();
            }
            // ****************************************************

            return sessions;
        }

        [HttpPost]
        public async Task<Response> AddAbsenceAsync(
            string studentIN, int sessionID, int year, int month, int day ){

            // initialise DB ****************************************
            var db = new ApplicationContext();
            // ******************************************************

            // INITILISE PREREQUIS **********************************
            var date = new DateTime(year, month, day).Date;
            var session = await db.Session.Where( s => s.ID == sessionID ).FirstAsync();
            var student = await db.Student.Where( s => s.InscriptionNumber == studentIN ).FirstAsync();
            var absence = new Absence(session , student, date, false);
            // *******************************************************

            // ADD TO DATABASE ***************************************
            await db.Absence.AddAsync( absence );
            await db.SaveChangesAsync();
            // *******************************************************
            
            return new Response(0, "done");
        }   

        [HttpPost]
        public async Task<Response> AddDelayAsync(
                string studentIN, int sessionID, int year, int month, 
                int day, int duration ){

            // initialise DB ****************************************
            var db = new ApplicationContext();
            // ******************************************************

            // INITILISE PREREQUIS **********************************
            var date = new DateTime(year, month, day).Date;
            var session = await db.Session.Where( s => s.ID == sessionID ).FirstAsync();
            var student = await db.Student.Where( s => s.InscriptionNumber == studentIN ).FirstAsync();
            var delay = new Delay(session , student, date, duration);
            // *******************************************************

            // ADD TO DATABASE ***************************************
            await db.Delay.AddAsync( delay );
            await db.SaveChangesAsync();
            // *******************************************************
            
            return new Response(0, "done");
        }   


        [HttpPost]
        public async Task<Session> AddSessionAsync(
            int fh, int fm, int th, int tm , int time_part,  int day_id,
            string former_id, int group_id

        ){
            var db = new ApplicationContext();
            var day = await db.Day.Where( d => d.ID == day_id ).FirstAsync();
            var group = await db.Group.Where ( g => g.ID == group_id ).FirstAsync();
            var former = await db.Former.Where( f => f.ID == former_id ).FirstAsync();
            var scholar_year = GetScholarYear();
            var schedule = await db.Schedule
                .Where( s => s.ScholarYear.ID == scholar_year.ID )
                .Where( s => s.Former.ID == former.ID )
                .FirstAsync();
            var session = new Session(group, day, schedule, fh, fm, th, tm, time_part, true);
            await db.Session.AddAsync( session );
            await db.SaveChangesAsync();

            return session;
        }

        [HttpPost]
        public async Task<Response> AddAbsenceHourAsync(
            string studentIN, int year, int month, int day_date, int session_id
             ){

            var db = new ApplicationContext();

            var date = new DateTime(year, month, day_date).Date;
            var student = await db.Student.Where( s => s.InscriptionNumber == studentIN ).FirstAsync();
            var session = await db.Session.Where( s => s.ID == session_id ).FirstAsync();

            var absence = new Absence(session , student, date, false);

            await db.Absence.AddAsync( absence );
            await db.SaveChangesAsync();
            
            return new Response(0, "done");
        }   


        [HttpPost]
        public async Task<Response> AddDelayHourAsync(
            string studentIN, int year, int month, int day_date, int duration, int session_id

        ){
            var db = new ApplicationContext();

            var date = new DateTime(year, month, day_date).Date;
            var student = await db.Student.Where( s => s.InscriptionNumber == studentIN ).FirstAsync();
            var session = await db.Session.Where( s => s.ID == session_id ).FirstAsync();

            var delay = new Delay(session , student, date, duration);

            await db.Delay.AddAsync( delay );
            await db.SaveChangesAsync();
            
            return new Response(0, "done");
        }   


        [HttpPost]
        public async Task<List<Day>> LoadDaysAsync(){
            var db = new ApplicationContext();
            return ( 
                await db.Day.ToListAsync() 
            );
        }     

        [HttpPost]
        public async Task<List<Former>> LoadFormersAsync() {
            var db = new ApplicationContext();
            return ( 
                await db.Former
                    .Where( f => f.is_actif == true )
                    .ToListAsync() 
            );
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
