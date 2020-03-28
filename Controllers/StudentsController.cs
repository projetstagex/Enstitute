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

namespace Enstitute.Controllers
{
    public class StudentsController : Controller
    {   
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ILogger<StudentsController> logger)
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

        public IActionResult Index()
        {
            if ( Authenticate() == 0){
                 var db = new ApplicationContext();
                var b = new Base();
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
            if (user.is_director || user.is_secretary ){
                formations = await db.Formation.ToListAsync();
            }
            //******************************************************
            
            //  si l'utilisateur est un formateur 
            // retorunez just les formations dont il a des groupes 
            else if (user.is_former){

                // get formateur de la BD
                var former = await db.Former.Where( f => f.ID == user.ID ).FirstAsync();
                // **************************************************

                // get les formations spécifique du formateurs
                var foo =   from formation in await db.Formation.ToListAsync()
                            join s in await db.Sector.ToListAsync() on formation.ID equals s.FormationID
                            join g in await db.Group.ToListAsync() on s.ID equals g.SectorID 
                            join ma in await db.ModuleAffectation.ToListAsync() on g.ID equals ma.GroupID 
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
            else if (user.is_former){
                
                // get formateur de la BD
                var former = await db.Former
                    .Where( f => f.ID == user.ID )
                    .FirstAsync();
                // **************************************************

                // get les grades spécifique du formateurs
                var foo =   from grade in await db.Grade.ToListAsync()
                            join s in await db.Sector.ToListAsync() 
                                on grade.ID equals s.GradeID
                            join g in await db.Group.ToListAsync() 
                                on s.ID equals g.SectorID 
                            join ma in await db.ModuleAffectation.ToListAsync() 
                                on g.ID equals ma.GroupID
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
            var user = await db.User
                .Where( u => u.ID == userID )
                .FirstAsync();
            //******************************************************
            
            // si l'utilisateur est un directeur ou secretaire 
            // retournez tous les grades dans la select
            int thisyear = DateTime.Now.Year;
            if ( user.is_director || user.is_secretary  ){
                var foo =   from g in await db.Group.ToListAsync()
                            join sy in await db.ScholarYear.ToListAsync() on g.ScholarYearID equals sy.ID
                            join s in await db.Sector.ToListAsync() on g.SectorID equals s.ID
                            where s.FormationID == formationID && s.GradeID == gradeId && sy.To == thisyear
                            select g ;
                groups = foo.Distinct().ToList();
            }
            //******************************************************
            
            //  si l'utilisateur est un formateur 
            // retorunez just les grades dont il a des groupes 
            else if (user.is_former){
                
                // get formateur de la BD
                var former = await db.Former
                    .Where( f => f.ID == user.ID )
                    .FirstAsync();
                // **************************************************

                // get les grades spécifique du formateurs
                var foo =   from g in await db.Group.ToListAsync()
                            join sy in await db.ScholarYear.ToListAsync() 
                                on g.ScholarYearID equals sy.ID
                            join s in await db.Sector.ToListAsync() 
                                on g.SectorID equals s.ID
                            join ma in await db.ModuleAffectation.ToListAsync() 
                                on g.ID equals ma.GroupID
                            where s.FormationID == formationID && s.GradeID == gradeId && ma.FormerID == former.ID && sy.To == thisyear
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
                .Where( g=>g.GroupID == group_id )
                .Include( g => g.Gender )
                .Include( g => g.Address )
                .Where( g => g.Is_training == true )
                .ToListAsync();
            //**********************************************************************
        }
    
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}