using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Enstitute
{
    public class SessionContext
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        [ForeignKey("Context")]
        public int ContextID {get;set;}
        public Context Context {get;set;}

        [Required]
        [ForeignKey("Session")]
        public int SessionID {get;set;}
        public Session Session {get;set;}

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}


        public SessionContext(){}
        public SessionContext(Context context, Session session, DateTime date){
            SessionID = session.ID;
            ContextID = context.ID;
            Session = session;
            Context = context; 
            Date = date;
        }

    }
}
