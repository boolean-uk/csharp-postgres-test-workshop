namespace UniversityApi.Models
{
    // TODO add decorators for columnes and table

    public class Enrollments
    {
        public int AcademicYear { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
