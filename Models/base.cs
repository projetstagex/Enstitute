using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Enstitute
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<ActionStudent> ActionStudent { get; set; }
        public DbSet<ActionGroup> ActionGroup { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Former> Former { get; set; }
        public DbSet<ModuleAffectation> ModuleAffectation { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Departement> Departement { get; set; }
        public DbSet<Day> Day { get; set; }
        public DbSet<Delay> Delay { get; set; }
        public DbSet<Absence> Absence { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<StudentEdited> StudentEdit { get; set; }
        public DbSet<GroupEdited> GroupEdit { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserSession> UserSession { get; set; }
        public DbSet<Secretary> Secretary { get; set; }
        public DbSet<Director> Director { get; set; }
        public DbSet<Precision> Precision { get; set; }
        public DbSet<Context> Context { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<SessionContext> SessionContext { get; set; }
        public DbSet<Formation> Formation { get; set; }
        public DbSet<ScholarYear> ScholarYear { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            // => options.UseNpgsql("Server=ec2-52-207-93-32.compute-1.amazonaws.com;Port=5432;Database=d5megfkuopjk61;User Id=izdpfrmpmfvnxs;Password=81f483b81f75d7f9fadf2f3793da9b2a853269f1bb1d60fdc56b6d20d7053440;");
            => options.UseSqlite($"Data Source=EnstituteDB.db");
    }
}