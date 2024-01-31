using Microsoft.AspNetCore.Mvc;
using UniversityApi.Repository;

namespace UniversityApi.Endpoints
{
    public static class UniversityApi
    {

        public static void ConfigureUniversityApiEndpoint(this WebApplication app)
        {
            var surgeryGroup = app.MapGroup("university");

            surgeryGroup.MapGet("/students", GetStudents);
            surgeryGroup.MapGet("/courses", GetCourses);
            surgeryGroup.MapGet("/enrollments/{studentId}", GetEnrollments);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetStudents(IRepository repository)
        {
            return TypedResults.Ok(await repository.GetStudents());
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetCourses(IRepository repository)
        {
            return TypedResults.Ok(await repository.GetCourses());
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetEnrollments(IRepository repository, int studentId)
        {
            return TypedResults.Ok(await repository.GetEnrollments(studentId));
        }
    }
}
