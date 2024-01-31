using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using UniversityApi.DTOs;
using UniversityApi.Models;
using UniversityApi.Repository;

namespace UniversityApi.Endpoints
{
    public static class UniversityApi
    {

        public static void ConfigureUniversityApiEndpoint(this WebApplication app)
        {
            var surgeryGroup = app.MapGroup("university");

            surgeryGroup.MapGet("/students", GetStudents);
            surgeryGroup.MapPost("/students", CreateStudent);
            surgeryGroup.MapGet("/courses", GetCourses);
            surgeryGroup.MapGet("/enrollments/{studentId}", GetEnrollments);
            surgeryGroup.MapPost("/enrollments/", EnrollStudent);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetStudents(IRepository repository)
        {
            var students = await repository.GetStudents();
            var studentDto = new List<StudentResponseDTO>();
            foreach(Student student in students)
            {
                studentDto.Add(new StudentResponseDTO(student));
            }
            return TypedResults.Ok(studentDto);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateStudent(CreateStudentPayload payload, IRepository repository)
        {
            // validate: a) payload has all of the properties we need (ie. they are NOT null)
            if(payload.Name == null || payload.Name == "")
            {
                return Results.BadRequest("A non-empty Name is required");
            }
            // validate: b) payload properties have acceptable values (ie. not empty, or within required ranges, etc...)

            Student? student = await repository.CreateStudent(payload.Name);
            if(student == null)
            {
                return Results.BadRequest("Failed to create student.");
            }

            return TypedResults.Ok(new StudentResponseDTO(student));
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetCourses(IRepository repository)
        {
            // GetCourses returns a List<Course> with each Course loading a List<Enrollments>, with each Enrollment loading the Student ->points back to Enrollments -> Courses
            var courses = await repository.GetCourses();
            var courseDto = new List<CourseResponseDTO>();
            foreach (Course course in courses)
            {
                courseDto.Add(new CourseResponseDTO(course));
            }
            return TypedResults.Ok(courseDto);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetEnrollments(IRepository repository, int studentId)
        {
            var enrollments = await repository.GetEnrollments(studentId);
            var enrollmentDto = new List<EnrollmentDTO>();
            foreach (Enrollments enrollment in enrollments)
            {
                enrollmentDto.Add(new EnrollmentDTO(enrollment));
            }
            return TypedResults.Ok(enrollmentDto);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> EnrollStudent(CreateEnrollmentPayload payload, IRepository repository)
        {
            // a) check for null properties on my payload => return bad request if missing fields
            if(payload.studentId == null || payload.courseId == null || payload.academicYear == null)
            {
                return Results.BadRequest("All fields studentId, courseId and academicYear are required");
            }

            // b) try to get student; return not found if null
            Student? student = await repository.GetStudent(payload.studentId);
            if(student == null)
            {
                return Results.NotFound("Student not found");
            }
            // c) try to get course; return not found if null
            Course? course = await repository.GetCourse(payload.courseId);
            if (course == null)
            {
                return Results.NotFound("Course not found");
            }

            // d) check academic year is within reasonable range of years
            if (payload.academicYear < DateTime.Now.Year - 1 || payload.academicYear > DateTime.Now.Year + 5)
            {
                return Results.BadRequest("Academic year must be -1 / +5 relative to current year.");
            }

            Enrollments? enrollment = await repository.CreateEnrollment(payload.academicYear, student, course);
            if(enrollment == null)
            {
                return Results.BadRequest("Failed to create enrollment; check if student and course exist and academic year is valid.");
            }

            return TypedResults.Ok(new EnrollmentDTO(enrollment));

        }
    }
}
