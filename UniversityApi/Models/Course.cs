using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityApi.Models
{
    // TODO add decorators for columnes and table
    [Table("courses")]
    public class Course
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }

        [Column("duration")]
        public int? Duration { get; set; }

        public ICollection<Enrollments> Enrollments { get; set; }
    }
}
