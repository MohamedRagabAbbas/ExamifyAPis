using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamifyApis.Migrations
{
    /// <inheritdoc />
    public partial class hhhh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Exams_ExamId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Students_StudentId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Exams_ExamId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_ExamId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_StudentId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Answers_ExamId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Grades",
                newName: "AttemptId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Answers",
                newName: "AttemptId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_StudentId",
                table: "Answers",
                newName: "IX_Answers_AttemptId");

            migrationBuilder.AddColumn<int>(
                name: "AttemptsNumber",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StudentAttempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAttempts_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentAttempts_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentAttemptsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attempts_StudentAttempts_StudentAttemptsId",
                        column: x => x.StudentAttemptsId,
                        principalTable: "StudentAttempts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_AttemptId",
                table: "Grades",
                column: "AttemptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attempts_StudentAttemptsId",
                table: "Attempts",
                column: "StudentAttemptsId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttempts_ExamId",
                table: "StudentAttempts",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttempts_StudentId",
                table: "StudentAttempts",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Attempts_AttemptId",
                table: "Answers",
                column: "AttemptId",
                principalTable: "Attempts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Attempts_AttemptId",
                table: "Grades",
                column: "AttemptId",
                principalTable: "Attempts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Attempts_AttemptId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Attempts_AttemptId",
                table: "Grades");

            migrationBuilder.DropTable(
                name: "Attempts");

            migrationBuilder.DropTable(
                name: "StudentAttempts");

            migrationBuilder.DropIndex(
                name: "IX_Grades_AttemptId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "AttemptsNumber",
                table: "Exams");

            migrationBuilder.RenameColumn(
                name: "AttemptId",
                table: "Grades",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "AttemptId",
                table: "Answers",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_AttemptId",
                table: "Answers",
                newName: "IX_Answers_StudentId");

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "Answers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_ExamId",
                table: "Grades",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ExamId",
                table: "Answers",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Exams_ExamId",
                table: "Answers",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Students_StudentId",
                table: "Answers",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Exams_ExamId",
                table: "Grades",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
