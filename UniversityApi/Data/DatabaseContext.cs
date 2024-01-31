using Microsoft.EntityFrameworkCore;
using UniversityApi.Models;

namespace UniversityApi.Data
{
    public class DatabaseContext: DbContext
    {
        private string _connectionString;
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {
            // loading the DefaultConnectionString value from the appsettings.json
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO remove this and show this
            //optionsBuilder.UseInMemoryDatabase(databaseName: "Database");
            // use postgresql db
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // set a primary composite key on the enrollments table
            // TODO remove this and show this
            modelBuilder.Entity<Enrollments>().HasKey(e => new { e.AcademicYear, e.StudentId, e.CourseId });

            // seed some students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "John Doe" },
                new Student { Id = 2, Name = "Jane Doe" },
                new Student { Id = 3, Name = "John Smith" },
                new Student { Id = 4, Name = "Jane Smith" }
            );

            // seed some courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Math" },
                new Course { Id = 2, Name = "Science" },
                new Course { Id = 3, Name = "English" },
                new Course { Id = 4, Name = "History" }
            );
            
            // seed some enrollments
            List<Enrollments> enrollments = new List<Enrollments>();
            enrollments.Add(new Enrollments { AcademicYear = 2020, StudentId = 1, CourseId = 1 });
            enrollments.Add(new Enrollments { AcademicYear = 2020, StudentId = 1, CourseId = 2 });
            enrollments.Add(new Enrollments { AcademicYear = 2020, StudentId = 1, CourseId = 3 });
            enrollments.Add(new Enrollments { AcademicYear = 2020, StudentId = 1, CourseId = 4 });
            enrollments.Add(new Enrollments { AcademicYear = 2020, StudentId = 2, CourseId = 1 });
            enrollments.Add(new Enrollments { AcademicYear = 2020, StudentId = 2, CourseId = 2 });
            enrollments.Add(new Enrollments { AcademicYear = 2020, StudentId = 2, CourseId = 3 });
            enrollments.Add(new Enrollments { AcademicYear = 2021, StudentId = 1, CourseId = 1 });
            enrollments.Add(new Enrollments { AcademicYear = 2021, StudentId = 3, CourseId = 2 });
            enrollments.Add(new Enrollments { AcademicYear = 2021, StudentId = 4, CourseId = 3 });

            // add to model
            modelBuilder.Entity<Enrollments>().HasData(enrollments);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollments> Enrollments { get; set; }
    }
}
