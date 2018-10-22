using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using testview.Models;

namespace testview.DAL
{
    public class SchoolContext: DbContext
    {
       public SchoolContext() : base("SchoolContext") {}
       public DbSet<Course> Courses { get; set; }
       public DbSet<Enrollment> Enrollments { get; set; }
       public DbSet<Student> Students { get; set; }
       public DbSet<Tasks> Tasks { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}