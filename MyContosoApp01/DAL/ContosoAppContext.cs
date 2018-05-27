using MyContosoApp01.Entities_V1;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MyContosoApp01.DAL
{
    public class ContosoAppContext : DbContext
    {
        public ContosoAppContext() : base("name=ContosoAppConnectionString")
        {
            //this.Configuration.LazyLoadingEnabled = false;

           // this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

             
        }

    }
}