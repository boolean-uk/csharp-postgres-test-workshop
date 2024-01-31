using UniversityApi.Models;

namespace UniversityApi.Repository
{
    public enum PreloadPolicy
    {
        PreloadRelations,
        DoNotPreloadRelations
    }

    public interface IRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student?> GetStudent(int studentId, PreloadPolicy preloadPolicy = PreloadPolicy.DoNotPreloadRelations);
        Task<Student?> CreateStudent(string name);

        Task<IEnumerable<Course>> GetCourses();
        Task<Course?> GetCourse(int courseId, PreloadPolicy preloadPolicy = PreloadPolicy.DoNotPreloadRelations);
        Task<IEnumerable<Enrollments>> GetEnrollments(int studentId);
        Task<Enrollments?> CreateEnrollment(int academicYear, Student student, Course course);
    }
}
