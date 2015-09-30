using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebUniversity.Models.DAL
{
    public class UniversityContext : DbContext
    {
        public UniversityContext()
            : base("name=DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        

        public DbSet<AcademicProgress> AcademicProgresses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<RecordBook> RecordBooks { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
 
    }
}