using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Enstitute.Controllers
{

    [ApiController]
    public class AbsencesApiController : Controller
    {   
         
        [HttpGet("{Controller}/Authenticate")]
        public async Task<Response> AuthenticateAsync(string username, string password){
            var b = new Base();
            var pass = b.ComputeHash256(password);
            var db = new ApplicationContext();

            var count = await db.User
                .Where( s => s.Username == username && s.Password == pass)
                .CountAsync();
            if (count != 0){
                
                var user = await db.User
                    .Where( u => u.Username == username 
                        && u.Password == pass 
                        && u.is_actif 
                        && u.is_secretary == true )
                    .FirstAsync();

                var student = await db.Student
                    .Where( s => s.Username == username 
                        && s.Password == pass  )
                    .FirstAsync();
                return new Response(0, "true");
            }
            return new Response( 1, "false" );
        }

        [HttpGet("{Controller}/GetTotalStudents")]
        public async Task<int> GetTotalStudentsAsync(string group_label){

            var db = new ApplicationContext();

            var group = await db.Group
                .Where( g => g.Label == group_label )
                .FirstAsync();
            if ( group == null ) 
                return -1;

            return await db.Student
                .Where( s => s.Group.Label == group_label )
                .CountAsync();
        }

        [HttpGet("{Controller}/GetSessions")]
        public async Task<List<Session>> GetSessionsAsync(int groupID){

            var db = new ApplicationContext();

            var group = await db.Group
                .Where( g => g.ID == groupID )
                .FirstAsync();

            return ( await db.Session
                .Where( s => s.Group == group )
                .ToListAsync() );
        }

        [HttpGet("{Controller}/GetStudents")]
        public async Task<List<Student>> GetStudentsAsync(int groupID){

            var db = new ApplicationContext();

            var group = await db.Group
                .Where( g => g.ID == groupID )
                .FirstAsync();
            return ( await db.Student
                .Where( s => s.Group == group )
                .ToListAsync() );
        }  

        [HttpGet("{Controller}/GetFormations")]
        public async Task<List<Formation>> GetFormationsAsync(){
            var db = new ApplicationContext();
            return ( await db.Formation.ToListAsync() );
        }

        [HttpGet("{Controller}/GetGrades")]
        public async Task<List<Grade>> GetGradesAsync(){
            var db = new ApplicationContext();
            return ( await db.Grade.ToListAsync() );
        }

        [HttpGet("{Controller}/GetGroups")]
        public async Task<List<Group>> GetGroupsAsync(  ){
            var db = new ApplicationContext();
            return ( await db.Group.ToListAsync() );
        }

        [HttpGet("{Controller}/GetGroup")]
        public async Task<Group> GetGroupAsync( int group_id ){
            var db = new ApplicationContext();
            return ( await db.Group.Where( g => g.ID == group_id ).FirstAsync() );
        }
        
        [HttpGet("{Controller}/GetGroupsCount")]
        public async Task<int> GetGroupCountsAsync(  ){
            var db = new ApplicationContext();
            return ( await db.Group.CountAsync() );
        }

        [HttpGet("{Controller}/AddAbsence")]
        public async Task<Response> AddAbsenceAsync( string student_name, float session_id, string group_label  )
        {
            var name = student_name.Split(null);
            var firstname = name[0];
            var lastname = name[1];
            var db = new ApplicationContext();

            var day_id = Convert.ToInt32( session_id )/5;
            var session_row = session_id - ( 5 * day_id );
            var group = db.Group.Where( s => s.Label == group_label ).FirstAsync();

            // var student = db.Student
            //     .Where( 
            //         s => s.FirstName == firstname 
            //         && s.LastName == lastname 
            //         && s.GroupID == group.ID)
            //     .FirstAsync();

            // var session = db.Session
            //     .Where( 
            //         s => s.TimePart == session_row 
            //         && s.DayID == day_id 
            //         && s.GroupID == group.ID 
            //         && s.is_catching == false )
            //     .FirstAsync();
            
            // var absence = new Absence(session, student, DateTime.Now, false);

            // await db.Absence.AddAsync( absence );
            // await db.SaveChangesAsync();
            return new Response(0, "done");
        }

        
    }
}