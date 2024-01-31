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
            return await _context.Students.ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<IEnumerable<Enrollments>> GetEnrollments(int studentId)
        {
            return await _context.Enrollments.Where(e => e.StudentId == studentId).ToListAsync();
        }
    }
}
