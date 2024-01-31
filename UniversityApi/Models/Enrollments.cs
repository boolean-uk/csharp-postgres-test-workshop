using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityApi.Models
{
    // TODO add decorators for columnes and table

    [Table("enrollments")]
    public class Enrollments
    {
        [Column("academic_year")]
        public int AcademicYear { get; set; }
        [Column("student_id")]
        public int StudentId { get; set; }
        public Student Student { get; set; }
        [Column("course_id")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
