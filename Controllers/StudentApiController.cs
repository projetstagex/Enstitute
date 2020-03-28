using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Enstitute.Controllers
{

    [ApiController]
    public class StudentApiController : Controller
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
                        && u.Password == pass )
                    .FirstAsync();

                var student = await db.Student
                    .Where( s => s.Username == username 
                        && s.Password == pass  )
                    .FirstAsync();
                return new Response(0, student.InscriptionNumber);
            }
            return new Response( 1, "false" );
        }


        [HttpGet("{Controller}/GetStudent")]
        public async Task<Student> GetStudentAsync( string student_id ){
            var db = new ApplicationContext();
            return( await db.Student
                    .Where( s => s.InscriptionNumber == student_id )
                    .Include(s => s.Address)
                    .Include( s => s.Gender)
                    .Include( s => s.Group )
                    .FirstAsync() );
        }
        
        [HttpGet("{Controller}/GetTotalAbsences")]
        public async Task<int> GetTotalAbsencesAsync(string student_id){
            var db = new ApplicationContext();
            return ( await db.Absence
                    .Where ( a => a.Student.InscriptionNumber == student_id )
                    .CountAsync() );
        }

        [HttpGet("{Controller}/GetTotalDelays")]
        public async Task<int> GetTotalDelaysAsync(string student_id){
            var db = new ApplicationContext();
            return ( await db.Delay
                    .Where ( a => a.Student.InscriptionNumber == student_id )
                    .CountAsync() );
        }

        [HttpGet("{Controller}/GetModules")]
        public async Task<List<ModuleAffectation>> GetModulesAsync(string student_id){
            var db = new ApplicationContext();
            var student = await db.Student
                .Where( s => s.InscriptionNumber == student_id )
                .FirstAsync();

            var modules = await db.ModuleAffectation
                    .Where( mf => mf.GroupID == student.GroupID )
                    .Include( mf => mf.Module)
                    .Include( mf => mf.Former )
                    .ToListAsync();
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
                    }
                }
                i = 0;
            }
            return modules; 
        }

    }
}