using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamifyApis.Migrations
{
    /// <inheritdoc />
    public partial class sadw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData
                (
                    "AspNetRoles",
                    new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                    new object[]
                    {
                        "1", "1", "Student", "STUDENT"
                    }
                );
            migrationBuilder.InsertData
                (
                    "AspNetRoles",
                    new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                    new object[]
                    {
                        "2", "2", "Teacher", "TEACHER"
                    }
                );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("AspNetRoles");
        }
    }
}
