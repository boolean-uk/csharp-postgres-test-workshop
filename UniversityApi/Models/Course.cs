namespace UniversityApi.Models
{
    // TODO add decorators for columnes and table
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Enrollments> Enrollments { get; set; }
    }
}
