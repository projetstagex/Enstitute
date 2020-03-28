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
using System.IO;
using OfficeOpenXml;

namespace Enstitute.Controllers
{

    public class AdminController : Controller
    {
        [HttpPost]
        public async Task<Response> Import(IFormFile formFile)  {  
            if (formFile == null || formFile.Length <= 0)  
            {  
                return new Response( -1, "formfile invalid" ) ;
            }  
        
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))  
            {  
                return new Response( -1, "extension de fichier est incorrect" ) ;
            }  
        
            var list = new List<Departement>();  
        
            using (var stream = new MemoryStream())  
            {  
                await formFile.CopyToAsync(stream);  
        
                using (var package = new ExcelPackage(stream))  
                {  
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];  
                    var rowCount = worksheet.Dimension.Rows;  
        
                    for (int row = 2; row <= rowCount; row++)  
                    {  
                        list.Add(new Departement  
                        {  
                            ID = int.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),  
                            Label = worksheet.Cells[row, 2].Value.ToString().Trim(),  
                        });  
                    }  
                }  
            }  
            
            Console.Write( list );

            return new Response(0, "OK");  
        }  
        [HttpGet("export")]  
        public async Task<IActionResult> ExportDepartementsAsync()  
        {  
            // query data from database  
            var db = new ApplicationContext();

            var list = await db.Departement.ToListAsync();

            var stream = new MemoryStream();  
        
            using (var package = new ExcelPackage(stream))  
            {  
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");  
                workSheet.Cells.LoadFromCollection(list, true);  
                package.Save();  
            }  
            stream.Position = 0;  
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";  
        
            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);  
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
        
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
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

        [HttpPost]
        public async Task<Module> LoadModuleAsync( int module_id ){
            var db = new ApplicationContext();
            return ( await db.Module.Where( m => m.ID == module_id ).FirstAsync() );
        }

        [HttpPost]
        public async Task<List<Gender>> LoadGendersAsync(  ){
            var db = new ApplicationContext();
            return ( await db.Gender.ToListAsync() );
        }
        
        [HttpPost]
        public async Task<Group> LoadGroupAsync( int group_id ){
            var db = new ApplicationContext();
            return ( await db.Group.Where( m => m.ID == group_id ).FirstAsync() );
        }

        [HttpPost]
        public async Task<Precision> LoadPrecisionAsync( int precision_id ){
            var db = new ApplicationContext();
            return ( await db.Precision.Include( p => p.Module ).Where( m => m.ID == precision_id ).FirstAsync() );
        }

        [HttpPost]
        public async Task<List<Student>> LoadStudentsAsync(string input){
            var db = new ApplicationContext();
            if ( (input + "abcd").Count() <= 4 ){
                return ( await db.Student
                    .Where( s => s.Is_training == true )
                    .Include( s => s.Group )
                    .ToListAsync() );
            }
            return ( await db.Student
                .Where( 
                    s => s.Is_training == true 
                    && ( 
                        s.FirstName.Contains(input) == true 
                        || s.LastName.Contains(input) == true  
                    ) 
                )
                .Include( s => s.Group )
                .ToListAsync() );
        }
        
        [HttpPost]
        public async Task<List<Student>> LoadStudentAsync( string student_id ){
            var db = new ApplicationContext();
            return ( await db.Student.Where( s=> s.ID == student_id ).Include( s => s.Group ).ToListAsync() );
        }
        
        [HttpPost]
        public async Task<List<Student>> LoadStudentsGroupAsync( int group_id ){
            var db = new ApplicationContext();
            return ( await db.Student.Where( s => s.GroupID == group_id ).ToListAsync() );
        }

        [HttpPost]
        public async Task<List<Module>> LoadModulesAsync( string input ){
            var db = new ApplicationContext();
            if ( (input + "abcd").Count() <= 4 ){
                return ( await db.Module.ToListAsync() );
            }
            return ( await db.Module
                .Where( 
                    m => m.Code.Contains(input ) == true 
                    || m.Title.Contains(input ) == true 
                )
                .ToListAsync() );
        }

        [HttpPost]
        public async Task<List<Precision>> LoadPrecisionsAsync( string input ){
            var db = new ApplicationContext();
            if ( (input + "abcd").Count() <= 4 ){
            return ( await db.Precision.Include( p => p.Module ).ToListAsync() );
            }
            return ( await db.Precision
                .Where( p => p.Label.Contains(input) == true
                        || p.Code.Contains(input) == true
                )
                .Include( p => p.Module )
                .ToListAsync() );

        }
        
        [HttpPost]
        public async Task<List<Context>> LoadContextsAsync( string input ){
            var db = new ApplicationContext();
            if ( (input + "abcd").Count() <= 4 ){
                return ( await db.Context
                    .Include( c => c.Precision )
                    .Include( c => c.Precision.Module )
                    .ToListAsync() );
            }
            return ( await db.Context
                .Where( 
                    c => c.Code.Contains(input) == true
                    || c.Label.Contains(input) == true
                )
                .Include( c => c.Precision )
                .Include( c => c.Precision.Module )
                .ToListAsync() );

        }

        [HttpPost]
        public async Task<List<Departement>> LoadDepartementsAsync( string input ){
            var db = new ApplicationContext();
            if ( (input + "abcd").Count() <= 4 ){
                return ( await db.Departement.ToListAsync() );
            }
            return ( await db.Departement
                .Where( 
                    d => d.Label.Contains(input) == true
                )
                .ToListAsync() );
        }

        [HttpPost]
        public async Task<List<Formation>> LoadFormationsAsync( string input ){
            var db = new ApplicationContext();
            if ( (input + "abcd").Count() <= 4 ){
                return ( await db.Formation.ToListAsync() );
            }
            return ( await db.Formation
                .Where( 
                    d => d.Label.Contains(input) == true
                )
                .ToListAsync() );
        }

        [HttpPost]
        public async Task<List<Grade>> LoadGradesAsync( string input ){
            var db = new ApplicationContext();
            if ( (input + "abcd").Count() <= 4 ){
                return ( await db.Grade.ToListAsync() );
            }
            return ( await db.Grade
                .Where( 
                    d => d.Label.Contains(input) == true
                    || d.Code.Contains(input) == true
                )
                .ToListAsync() );
        }

        [HttpPost]
        public async Task<Grade> LoadGradeAsync( int grade_id ){
            var db = new ApplicationContext();
            return ( await db.Grade.Where( g => g.ID == grade_id ).FirstAsync() );
        }
        
        [HttpPost]
        public async Task<Departement> LoadDepartementAsync( int departement_id ){
            var db = new ApplicationContext();
            return ( await db.Departement.Where( g => g.ID == departement_id ).FirstAsync() );
        }
        
        [HttpPost]
        public async Task<List<Sector>> LoadSectorsAsync( string input ){
            var db = new ApplicationContext();
            if ( (input + "abcd").Count() <= 4 ){
                return ( await db.Sector.ToListAsync() );
            }
            return ( await db.Sector
                .Where( 
                    d => d.Label.Contains(input) == true
                    || d.Code.Contains(input) == true
                )
                .ToListAsync() );
        }

        [HttpPost]
        public async Task<List<ScholarYear>> LoadScholarYearsAsync(  ){
            var db = new ApplicationContext();
            return ( await db.ScholarYear.ToListAsync() );
        }

        [HttpPost]
        public async Task<List<Group>> LoadGroupsAsync( string input ){
            var db = new ApplicationContext();
            if ( (input + "abcd").Count() <= 4 ){
                return ( await db.Group.Include( g => g.Sector ).ToListAsync() );
            }
            return ( await db.Group
                .Where ( 
                    g => g.Label.Contains(input) == true 
                    || g.Code.Contains(input) == true
                )
                .Include( g => g.Sector )
                .ToListAsync() );

        }

        [HttpPost]
        public async Task<List<Formation>> LoadFormationsDepartementAsync( int departement_id ){
            var db = new ApplicationContext();
            return ( await db.Formation.Where( d => d.DepartementID == departement_id ).ToListAsync() );
        }

        [HttpPost]
        public async Task<List<Context>> LoadContextsPrecisionAsync( int precision_id ){
            var db = new ApplicationContext();
            return ( await db.Context.Where( c => c.PrecisionID == precision_id ).ToListAsync() );
        }

        [HttpPost]
        public async Task<List<Precision>> LoadPrecisionsModuleAsync( int module_id ){
            var db = new ApplicationContext();
            return ( await db.Precision.Where( p => p.ModuleID == module_id ).ToListAsync() );
        }
        

        // ******************************** MODULE **************************************************************
        [HttpPost]
        public async Task<Response> AddModuleAsync( string title, string code, int multiplier ){
           var db = new ApplicationContext();
           var count = await db.Module
                .Where( m => m.Code == code || m.Title == title )
                .CountAsync();
            if ( count == 0 ){
                var module = new Module( multiplier, title, code );
                await db.AddAsync( module );
                await db.SaveChangesAsync();
                return new Response(0, "done");
            } 
            return new Response(1, "fail");
        }

        [HttpPost]
        public async Task<Response> EditModuleAsync( int module_id, string title, string code, int multiplier ){
            var db = new ApplicationContext();

            var module = await db.Module.Where( m => m.ID == module_id ).FirstAsync();

            module.Title = title;
            module.Code = code;
            module.Multiplier = multiplier;
            db.Module.Update(module); 
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }
        
        [HttpPost]
        public async Task<Response> DeleteModuleAsync( int module_id ){
            var db = new ApplicationContext();
            var module = await db.Module.Where( m => m.ID == module_id ).FirstAsync();
            db.Module.Remove(module);
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }

        // *******************************************************************************************************
        
        // ******************************** PRECISION ************************************************************
        [HttpPost]
        public async Task<Response> AddPrecisionAsync( int module_id, string label, string code){
            var db = new ApplicationContext();
            var count = await db.Precision
                .Where( m => (m.Code == code || m.Label == label) && m.ModuleID == module_id )
                .CountAsync();
            if ( count == 0 ){
                var module = await db.Module.Where( m => m.ID == module_id ).FirstAsync();
                var precision = new Precision(label, code, module );
                await db.AddAsync( precision );
                await db.SaveChangesAsync();
                return new Response(0, "done");
            } 
            return new Response(1, "fail");
        }

        [HttpPost]
        public async Task<Response> EditPrecisionAsync( int precision_id, string label, string code ){
            var db = new ApplicationContext();
            var precision = await db.Precision.Where( p => p.ID == precision_id ).FirstAsync();
            precision.Label = label;
            precision.Code = code;
            db.Precision.Update(precision);
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }
        
        [HttpPost]
        public async Task<Response> DeletePrecisionAsync( int precision_id ){
            var db = new ApplicationContext();
            var precision = await db.Precision.Where( m => m.ID == precision_id ).FirstAsync();
            db.Precision.Remove(precision);
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }
        // *******************************************************************************************************

        // ******************************** CONTEXT **************************************************************
        [HttpPost]
        public async Task<Response> AddContextAsync( int precision_id, string label, string code){
            var db = new ApplicationContext();
            var count = await db.Context
                .Where( m => (m.Code == code || m.Label == label) && m.PrecisionID == precision_id  )
                .CountAsync();
            if ( count == 0 ){
                var precision = await db.Precision.Where( m => m.ID == precision_id ).FirstAsync();
                var context = new Context(label, precision, code );
                await db.AddAsync( context );
                await db.SaveChangesAsync();
                return new Response(0, "done");
            } 
            return new Response(1, "fail");
        }

        [HttpPost]
        public async Task<Response> EditContextAsync( int context_id, string label, string code ){
            var db = new ApplicationContext();
            var context = await db.Context.Where( c => c.ID == context_id ).FirstAsync();
            context.Label = label;
            context.Code = code;
            db.Context.Update(context);
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }
        
        [HttpPost]
        public async Task<Response> DeleteContextAsync( int context_id ){
            var db = new ApplicationContext();
            var context = await db.Context.Where( m => m.ID == context_id ).FirstAsync();
            db.Context.Remove(context);
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }
        // *******************************************************************************************************

        // ******************************** DEPARTEMENT **********************************************************
        [HttpPost]
        public async Task<Response> AddDepartementAsync( string label){
            var db = new ApplicationContext();
            var count = await db.Departement.Where( m => m.Label == label ).CountAsync();
            if ( count == 0 ){
                var departement = new Departement(label);
                await db.AddAsync( departement );
                await db.SaveChangesAsync();
                return new Response(0, "done");
            } 
            return new Response(1, "fail");
        }

        [HttpPost]
        public async Task<Response> EditDepartementAsync( int departement_id, string label ){
            var db = new ApplicationContext();
            var departement = await db.Departement.Where( c => c.ID == departement_id ).FirstAsync();
            departement.Label = label;
            db.Departement.Update(departement);
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }
        
        [HttpPost]
        public async Task<Response> DeleteDepartementAsync( int departement_id ){
            var db = new ApplicationContext();
            var departement = await db.Departement.Where( m => m.ID == departement_id ).FirstAsync();
            db.Departement.Remove(departement);
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }
        // *******************************************************************************************************

        // ********************************** FORMATION **********************************************************
        [HttpPost]
        public async Task<Response> AddFormationAsync( string label, string code, int totalYears, int departement_id){
            var db = new ApplicationContext();
            var count = await db.Formation
                .Where( m => (m.Code == code || m.Label == label) && m.DepartementID == departement_id )
                .CountAsync();
            if ( count == 0 ){
                var departement = await db.Departement.Where( d => d.ID == departement_id ).FirstAsync();
                var formation = new Formation(totalYears, label, code, departement );
                await db.AddAsync( formation );
                await db.SaveChangesAsync();
                return new Response(0, "done");
            }
            return new Response(1, "fail");
        }

        [HttpPost]
        public async Task<Response> EditFormationAsync( int formation_id, string label, string code, int totalYears){
            var db = new ApplicationContext();
            var formation = await db.Formation.Where( f => f.ID == formation_id ).FirstAsync();
            formation.Label = label;
            formation.Code = code;
            formation.TotalYears = totalYears;
            db.Formation.Update(formation);
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }

        [HttpPost]
        public async Task<Response> DeleteFormationAsync( int formation_id ){
            var db = new ApplicationContext();
            var formation = await db.Formation.Where( m => m.ID == formation_id ).FirstAsync();
            db.Formation.Remove(formation);
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }
        // *******************************************************************************************************

        // ******************************** GRADE ****************************************************************
        [HttpPost]
        public async Task<Response> AddGradeAsync( string label, string code ){
            var db = new ApplicationContext();
            var count = await db.Grade.Where( m => m.Label == label && m.Code == code ).CountAsync();
            if ( count == 0 ){
                var grade = new Grade(label, code);
                await db.AddAsync( grade );
                await db.SaveChangesAsync();
                return new Response(0, "done");
            } 
            return new Response(1, "fail");
        }

        [HttpPost]
        public async Task<Response> EditGradeAsync( int grade_id, string label, string code ){
            var db = new ApplicationContext();
            var grade = await db.Grade.Where( c => c.ID == grade_id ).FirstAsync();
            grade.Label = label;
            grade.Code = code;
            db.Grade.Update(grade);
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }
        
        [HttpPost]
        public async Task<Response> DeleteGradeAsync( int grade_id ){
            var db = new ApplicationContext();
            var grade = await db.Grade.Where( m => m.ID == grade_id ).FirstAsync();
            db.Grade.Remove(grade);
            await db.SaveChangesAsync();
            return new Response(0, "done");
        }
        // *******************************************************************************************************

        // ******************************** STUDENT **************************************************************
        [HttpPost]
        public async Task<Response> AddStudentAsync( string first_name, string last_name,
                string email, string phone, string nationality, string gender_label, string group_label,  string street, string appartement,
                string zipcode, string city, int day, int month, int year, string password, string nic){
            
            var db = new ApplicationContext();
            var count = 0; // *
            if ( count == 0 ){
                var abs = new Student();
                var group = await db.Group.Where( g => g.Label == group_label ).FirstAsync();
                var gender = await db.Gender.Where( g => g.Label == gender_label ).FirstAsync();
                var address = new Address(street, appartement, zipcode, city);
                var username = last_name + first_name + year;
                string id;
                string inscription_number;
                while ( true ){
                    id = abs.GenrateID();
                    inscription_number = abs.GenrateID();
                    var count2 = await db.Student
                        .Where( s => s.InscriptionNumber == inscription_number || s.ID == id )
                        .CountAsync();
                    if ( count2 == 0 ) 
                        break;
                }

                var user_id = GetUserID();
                var user = await db.User.Where( u=> u.ID == user_id ).FirstAsync();    
                var student = new Student(username, password, first_name, last_name, year, month, day, 
                    gender, address, phone, email, nic, group, nationality, id, inscription_number);

                if ( user.is_secretary ){
                    student.Is_training = false;
                    student.is_actif = false;
                }

                await db.AddAsync( student );
                await db.SaveChangesAsync();
                return new Response(0, "done");
            }
            return new Response(1, "fail");
        }


        [HttpPost]
        public async Task<Response> RemoveStudentAsync( string student_id ){
            
            var db = new ApplicationContext();
            var student = await db.Student.Where( s => s.ID == student_id ).FirstAsync();
            
            db.Student.Remove( student );
            await db.SaveChangesAsync();

            return new Response(0, "done");
        }


        [HttpPost]
        public async Task<Response> EditStudentAsync( string student_id, string firstname, string lastname,
            string email, string phone, string nationality, string group_label ){
            
            var db = new ApplicationContext();
            var student = await db.Student.Where( s => s.ID == student_id ).FirstAsync();
            var group = await db.Group.Where( g => g.Label == group_label ).FirstAsync();
            var user = await db.User.Where( u => u.ID == GetUserID() ).FirstAsync();
            if ( user.is_secretary ){
                var student_edited = new StudentEdited(firstname, lastname, phone, email, group, nationality, student);
                await db.StudentEdit.AddAsync( student_edited );
                var secretary = await db.Secretary.Where( s => s.ID == user.ID ).FirstAsync();
                var date = DateTime.Now;  
                var action_student = new ActionStudent( "edit student", date, secretary, student_edited  );
                await db.ActionStudent.AddAsync( action_student );
            }
            else {
                student.Phone = phone;
                student.Email = email;
                student.Nationality = nationality;
                student.FirstName = firstname;
                student.LastName = lastname;
                db.Student.Update( student );
            }
           
            await db.SaveChangesAsync();

            return new Response(0, "done");
        }

            
        // *******************************************************************************************************

        // ******************************** GROUP ****************************************************************
        [HttpPost]
        public async Task<Response> AddGroupAsync( string code, string label, string scholaryear_code, string sector_label ){
            
            var db = new ApplicationContext();
            var count = 0; // *
            if ( count == 0 ){
                var scholaryear = await db.ScholarYear.Where( sy => sy.Code == scholaryear_code ).FirstAsync();
                var sector = await db.Sector.Where( s => s.Label == sector_label ).FirstAsync();
                var group = new Group( label, sector, code, scholaryear );
                await db.Group.AddAsync( group );
                await db.SaveChangesAsync();
                return new Response(0, "done");
            }                
            return new Response(1, "fail");
        }

        [HttpPost]
        public async Task<Response> EditGroupAsync( int id, string code, string label){
            
            var db = new ApplicationContext();

            var user = await db.User.Where( u => u.ID == GetUserID() ).FirstAsync();
            var group = await db.Group.Where( g => g.ID == id ).FirstAsync();

            if ( user.is_secretary ){
                var secretary = await db.Secretary.Where( s => s.ID == user.ID ).FirstAsync();
                var group_edited = new GroupEdited(code, label, group);
                await db.GroupEdit.AddAsync( group_edited );
                var date = DateTime.Now;  
                var action_group = new ActionGroup("Group Edited", date, secretary, group_edited );
                await db.ActionGroup.AddAsync( action_group );
            }
            else {
                group.Label = label;
                group.Code = code;
                db.Group.Update( group );
            }
            await db.SaveChangesAsync();

            return new Response(0, "done");
        }

        [HttpPost]
        public async Task<Response> RemoveGroupAsync( int group_id ){
            
            var db = new ApplicationContext();

            var group = await db.Group.Where( g => g.ID == group_id ).FirstAsync();
            db.Group.Remove( group );

            await db.SaveChangesAsync();

            return new Response(0, "done");
        }

        // *******************************************************************************************************

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
            if ( Authenticate() == 0 ) 
            {
                var db = new ApplicationContext();
                var user = db.User
                    .Where( u => u.ID == GetUserID() )
                    .First();
                if ( user.is_director || user.is_staff ){
                    ViewBag.director = "True";
                    ViewBag.UserName = HttpContext.Session.GetString("userName");
                    ViewBag.acces_admin = "True";
                    ViewBag.director = "True";
                    return View();
                }
                else if ( user.is_secretary ){
                    ViewBag.director = "False";
                    ViewBag.UserName = HttpContext.Session.GetString("userName");
                    ViewBag.acces_admin = "True";
                    return View();
                }
                else {
                    return Redirect("Home");
                }
            }   
            return Redirect("Authentification");
        }
    
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(){
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
