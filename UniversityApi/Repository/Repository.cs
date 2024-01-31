using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Models;

namespace UniversityApi.Repository
{
    public class Repository: IRepository
    {
        private readonly DatabaseContext _context;

        public Repository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _context.Students.Include(s => s.Enrollments).ThenInclude(e => e.Course).ToListAsync();
        }

        public async Task<Student?> GetStudent(int studentId, PreloadPolicy preloadPolicy = PreloadPolicy.DoNotPreloadRelations)
        {

            switch(preloadPolicy)
            {
                case PreloadPolicy.PreloadRelations:
                    return await _context.Students.Include(s => s.Enrollments).ThenInclude(e => e.Course).FirstOrDefaultAsync(s => s.Id == studentId);
                case PreloadPolicy.DoNotPreloadRelations:
                    return await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
                default:
                    return null;
            }
        }

        public async Task<Student?> CreateStudent(string name)
        {
            if (name == "") return null;
            var student = new Student { Name = name };
            await _context.Students.AddAsync(student);
            return student;
        }

        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course?> GetCourse(int courseId, PreloadPolicy preloadPolicy = PreloadPolicy.DoNotPreloadRelations)
        {

            switch (preloadPolicy)
            {
                case PreloadPolicy.PreloadRelations:
                    return await _context.Courses.Include(c => c.Enrollments).ThenInclude(e => e.Student).FirstOrDefaultAsync(c => c.Id == courseId);
                case PreloadPolicy.DoNotPreloadRelations:
                    return await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
                default:
                    return null;
            }
        }

        public async Task<IEnumerable<Enrollments>> GetEnrollments(int studentId)
        {
            return await _context.Enrollments.Where(e => e.StudentId == studentId).ToListAsync();
        }

        public async Task<Enrollments?> CreateEnrollment(int academicYear, Student student, Course course)
        {
            if(student == null || course == null) return null;
            var enrollment = new Enrollments { 
                AcademicYear = academicYear, 
                StudentId = student.Id, 
                Student = student, 
                CourseId = course.Id, 
                Course = course 
            };

            try
            {
                await _context.Enrollments.AddAsync(enrollment);
            } catch(Exception ex)
            {
                // failed to create enrollment, maybe the ids are wrong for student or course
                return null;
            }
            return enrollment;
        }
    }
}
