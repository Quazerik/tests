using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TestsApp.Migrations
{
    public partial class M2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeInSeconds",
                table: "Tests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    TestId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    RightAnswer = table.Column<string>(nullable: true),
                    SerializedAnswerVariants = table.Column<string>(nullable: true),
                    SerializedRightAnswersNums = table.Column<string>(nullable: true),
                    RightAnswerNum = table.Column<int>(nullable: true),
                    SerializedLeft = table.Column<string>(nullable: true),
                    SerializedRight = table.Column<string>(nullable: true),
                    SerializedConnections = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => new {x.TestId, x.Number});
                    table.ForeignKey(
                        name: "FK_Questions_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TestId = table.Column<int>(nullable: false),
                    StudentId = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    FinishTime = table.Column<DateTime>(nullable: true),
                    Result = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResults_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestResults_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    TestResultId = table.Column<int>(nullable: false),
                    QuestionNumber = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    SerializedAnswersNums = table.Column<string>(nullable: true),
                    ChosenAnswerNum = table.Column<int>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    SerializedConnections = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => new {x.TestResultId, x.QuestionNumber});
                    table.ForeignKey(
                        name: "FK_Answers_TestResults_TestResultId",
                        column: x => x.TestResultId,
                        principalTable: "TestResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_StudentId",
                table: "TestResults",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_TestId",
                table: "TestResults",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "TestResults");

            migrationBuilder.DropColumn(
                name: "TimeInSeconds",
                table: "Tests");
        }
    }
}
