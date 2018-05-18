using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;

namespace MyContosoApp01.DAL
{
    public class ContosoAppContext : DbContext
    {
        public ContosoAppContext() : base()
        {
            //this.Configuration.LazyLoadingEnabled = false;

           // this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}