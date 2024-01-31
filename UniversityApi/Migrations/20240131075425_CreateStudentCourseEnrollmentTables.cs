using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateStudentCourseEnrollmentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "enrollments",
                columns: table => new
                {
                    academic_year = table.Column<int>(type: "integer", nullable: false),
                    student_id = table.Column<int>(type: "integer", nullable: false),
                    course_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enrollments", x => new { x.academic_year, x.student_id, x.course_id });
                    table.ForeignKey(
                        name: "FK_enrollments_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_enrollments_students_student_id",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "courses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Math" },
                    { 2, "Science" },
                    { 3, "English" },
                    { 4, "History" }
                });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "John Doe" },
                    { 2, "Jane Doe" },
                    { 3, "John Smith" },
                    { 4, "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "enrollments",
                columns: new[] { "academic_year", "course_id", "student_id" },
                values: new object[,]
                {
                    { 2020, 1, 1 },
                    { 2020, 2, 1 },
                    { 2020, 3, 1 },
                    { 2020, 4, 1 },
                    { 2020, 1, 2 },
                    { 2020, 2, 2 },
                    { 2020, 3, 2 },
                    { 2021, 1, 1 },
                    { 2021, 2, 3 },
                    { 2021, 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_course_id",
                table: "enrollments",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_student_id",
                table: "enrollments",
                column: "student_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enrollments");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "students");
        }
    }
}
