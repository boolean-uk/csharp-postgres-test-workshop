using UniversityApi.Models;

namespace UniversityApi.DTOs
{
    class StudentResponseDTO
    {
        // define all of the properties that we want to return to the client
        public int Id { get; set; }
        public string Name { get; set; }

        public List<StudentEnrollmentDTO> Enrollments { get; set; } = new List<StudentEnrollmentDTO>();

        public StudentResponseDTO(Student student)
        {
            Id = student.Id;
            Name = student.Name;
            foreach (Enrollments enrollment in student.Enrollments)
            {
                Enrollments.Add(new StudentEnrollmentDTO(enrollment));
            }
        }
    }

    class CourseResponseDTO
    {
        // define all of the properties that we want to return to the client
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Duration { get; set; }

        public List<CourseEnrollmentDTO> Enrollments { get; set; } = new List<CourseEnrollmentDTO>();

        public CourseResponseDTO(Course course)
        {
            Id = course.Id;
            Name = course.Name;
            Duration = course.Duration;
            foreach (Enrollments enrollment in course.Enrollments)
            {
                Enrollments.Add(new CourseEnrollmentDTO(enrollment));
            }
        }
    }

    class StudentEnrollmentDTO
    {
        public int AcademicYear { get; set; }

        public CourseDTO Course { get; set; }

        public StudentEnrollmentDTO(Enrollments enrollment)
        {
            AcademicYear = enrollment.AcademicYear;
            Course = new CourseDTO(enrollment.Course);
        }
    }

    class CourseEnrollmentDTO
    {
        public int AcademicYear { get; set; }

        public StudentDTO Student { get; set; }

        public CourseEnrollmentDTO(Enrollments enrollment)
        {
            AcademicYear = enrollment.AcademicYear;
            Student = new StudentDTO(enrollment.Student);
        }
    }
    class EnrollmentDTO
    {
        public int AcademicYear { get; set; }

        public StudentDTO Student { get; set; }
        public CourseDTO Course { get; set; }

        public EnrollmentDTO(Enrollments enrollment)
        {
            AcademicYear = enrollment.AcademicYear;
            Student = new StudentDTO(enrollment.Student);
            Course = new CourseDTO(enrollment.Course);
        }
    }

    class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Duration { get; set; }

        public CourseDTO(Course course)
        {
            Id = course.Id;
            Name = course.Name;
            Duration = course.Duration;
        }
    }

    class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public StudentDTO(Student student)
        {
            Id = student.Id;
            Name = student.Name;
        }
    }

}
