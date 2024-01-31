using UniversityApi.Models;

namespace UniversityApi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<IEnumerable<Course>> GetCourses();
        Task<IEnumerable<Enrollments>> GetEnrollments(int studentId);
    }
}
