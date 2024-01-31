using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UniversityApi.Models
{
    // TODO add decorators for columnes and table

    [Table("students")]
    public class Student
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public ICollection<Enrollments> Enrollments { get; set; }
    }
}
