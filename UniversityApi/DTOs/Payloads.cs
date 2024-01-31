namespace UniversityApi.DTOs
{
    public record CreateStudentPayload(string Name);
    public record CreateEnrollmentPayload(int studentId, int courseId, int academicYear);

}
