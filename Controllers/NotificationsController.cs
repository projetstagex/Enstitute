using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Enstitute.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Enstitute.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly ILogger<NotificationsController> _logger;

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
        public NotificationsController(ILogger<NotificationsController> logger)
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

        public IActionResult Index()
        {
            if ( Authenticate() == 0 ) 
            {
                var db = new ApplicationContext();
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


        public async Task<List<ActionGroup>> LoadGroupsNotifsAsync(){
            var db = new ApplicationContext();
            var actions = await db.ActionGroup
            .Include( a => a.GroupEdited )
            .Include( a => a.Secretary )
            .ToListAsync(); 
            return actions;
        }
        public async Task<List<ActionStudent>> LoadStudentsNotifsAsync(){
            var db = new ApplicationContext();
            var actions = await db.ActionStudent
                .Include( a => a.StudentEdited )
                .Include( a => a.Secretary )
                .ToListAsync(); 
            return actions;
        }

        public async Task<Group> LoadOriginalAsync( int id ) {
            var db = new ApplicationContext();

            var action_group = await db.ActionGroup.Where( a => a.ID == id ).FirstAsync();

            var group_edited =  await db.GroupEdit
                .Where( g => g.ID == action_group.GroupEditedID )
                .FirstAsync();
            var group = await db.Group
                .Where( g => g.ID == group_edited.GroupID )
                .FirstAsync();
                
            return (group);
        }

        public async Task<GroupEdited> LoadModifiedAsync( int id ) {
            var db = new ApplicationContext();

            var action_group = await db.ActionGroup.Where( a => a.ID == id ).FirstAsync();

            var group_edited =  await db.GroupEdit
                .Where( g => g.ID == action_group.GroupEditedID )
                .FirstAsync();
           
            return (group_edited);
        }

        public async Task<Response> SaveAsync( int modification_id, int original_id, int action_id ) {
            var db = new ApplicationContext();

            var action_group = await db.ActionGroup
                .Where( a => a.ID == action_id )
                .FirstAsync();
            
            var group = await db.Group
                .Where( g => g.ID == original_id )
                .FirstAsync();
            
            var edited_group = await db.GroupEdit
                .Where( g => g.ID == modification_id )
                .FirstAsync();
            
            group.Label = edited_group.Label;
            group.Code = edited_group.Code;

            db.Group.Update( group );
            db.GroupEdit.Remove( edited_group );
            db.ActionGroup.Remove( action_group );

            await db.SaveChangesAsync();

            return new Response(0, "done");
        }

        public async Task<Response> DeleteAsync( int modification_id, int action_id ) {
            var db = new ApplicationContext();

            var action_group = await db.ActionGroup
                .Where( a => a.ID == action_id )
                .FirstAsync();
            
           
            var edited_group = await db.GroupEdit
                .Where( g => g.ID == modification_id )
                .FirstAsync();
            
            db.GroupEdit.Remove( edited_group );
            db.ActionGroup.Remove( action_group );

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
