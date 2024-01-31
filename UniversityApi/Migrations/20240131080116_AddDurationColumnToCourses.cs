using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationColumnToCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "duration",
                table: "courses",
                type: "integer",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 1,
                column: "duration",
                value: 0);

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 2,
                column: "duration",
                value: 0);

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 3,
                column: "duration",
                value: 0);

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 4,
                column: "duration",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "duration",
                table: "courses");
        }
    }
}
