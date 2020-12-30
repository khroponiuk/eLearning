using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eLearning.Core.Migrations
{
    public partial class AddLabSubmissionsAndQuizResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GraphEdges",
                keyColumn: "Id",
                keyValue: new Guid("4342b271-fc3d-4a45-a05e-44f2e34a68bc"));

            migrationBuilder.DeleteData(
                table: "GraphNodes",
                keyColumn: "Id",
                keyValue: new Guid("0475dbca-7b81-4a7d-8a86-fa45e38da9ba"));

            migrationBuilder.DeleteData(
                table: "GraphNodes",
                keyColumn: "Id",
                keyValue: new Guid("7aa3e7ef-e207-4b36-bace-86ab9598ce1c"));

            migrationBuilder.DeleteData(
                table: "Graphs",
                keyColumn: "Id",
                keyValue: new Guid("7dbc7ce9-f34b-478f-9591-e2c190c402cc"));

            migrationBuilder.CreateTable(
                name: "LabSubmissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LabId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    RepoLink = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    SubmissionTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabSubmissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizResults",
                columns: table => new
                {
                    QuizAttemptId = table.Column<int>(nullable: false),
                    ExternalQuizId = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    NumberOfCorrectAnswers = table.Column<int>(nullable: false),
                    NumberOfWrongAnswers = table.Column<int>(nullable: false),
                    NumberOfUnanswered = table.Column<int>(nullable: false),
                    TotalScore = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizResults", x => x.QuizAttemptId);
                });

            migrationBuilder.InsertData(
                table: "Graphs",
                columns: new[] { "Id", "Name", "Scale", "TranslateX", "TranslateY", "Type" },
                values: new object[] { new Guid("f3ae22cd-2094-4859-abbd-be8e8224c686"), "Main graph", 1.0, 0.0, 0.0, 0 });

            migrationBuilder.InsertData(
                table: "GraphEdges",
                columns: new[] { "Id", "GraphId", "SourceNodeId", "TargetNodeId" },
                values: new object[] { new Guid("ea66eada-ee6d-450a-809a-65302cc4405d"), new Guid("f3ae22cd-2094-4859-abbd-be8e8224c686"), new Guid("9451b81d-6f09-4d49-9f0d-838168fa432f"), new Guid("30c19e72-7c4c-4295-9622-7ceedbaa1819") });

            migrationBuilder.InsertData(
                table: "GraphNodes",
                columns: new[] { "Id", "GraphId", "Name", "X", "Y" },
                values: new object[] { new Guid("9451b81d-6f09-4d49-9f0d-838168fa432f"), new Guid("f3ae22cd-2094-4859-abbd-be8e8224c686"), "Intro", 550, 270 });

            migrationBuilder.InsertData(
                table: "GraphNodes",
                columns: new[] { "Id", "GraphId", "Name", "X", "Y" },
                values: new object[] { new Guid("30c19e72-7c4c-4295-9622-7ceedbaa1819"), new Guid("f3ae22cd-2094-4859-abbd-be8e8224c686"), "Topic", 750, 370 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabSubmissions");

            migrationBuilder.DropTable(
                name: "QuizResults");

            migrationBuilder.DeleteData(
                table: "GraphEdges",
                keyColumn: "Id",
                keyValue: new Guid("ea66eada-ee6d-450a-809a-65302cc4405d"));

            migrationBuilder.DeleteData(
                table: "GraphNodes",
                keyColumn: "Id",
                keyValue: new Guid("30c19e72-7c4c-4295-9622-7ceedbaa1819"));

            migrationBuilder.DeleteData(
                table: "GraphNodes",
                keyColumn: "Id",
                keyValue: new Guid("9451b81d-6f09-4d49-9f0d-838168fa432f"));

            migrationBuilder.DeleteData(
                table: "Graphs",
                keyColumn: "Id",
                keyValue: new Guid("f3ae22cd-2094-4859-abbd-be8e8224c686"));

            migrationBuilder.InsertData(
                table: "Graphs",
                columns: new[] { "Id", "Name", "Scale", "TranslateX", "TranslateY", "Type" },
                values: new object[] { new Guid("7dbc7ce9-f34b-478f-9591-e2c190c402cc"), "Main graph", 1.0, 0.0, 0.0, 0 });

            migrationBuilder.InsertData(
                table: "GraphEdges",
                columns: new[] { "Id", "GraphId", "SourceNodeId", "TargetNodeId" },
                values: new object[] { new Guid("4342b271-fc3d-4a45-a05e-44f2e34a68bc"), new Guid("7dbc7ce9-f34b-478f-9591-e2c190c402cc"), new Guid("7aa3e7ef-e207-4b36-bace-86ab9598ce1c"), new Guid("0475dbca-7b81-4a7d-8a86-fa45e38da9ba") });

            migrationBuilder.InsertData(
                table: "GraphNodes",
                columns: new[] { "Id", "GraphId", "Name", "X", "Y" },
                values: new object[] { new Guid("7aa3e7ef-e207-4b36-bace-86ab9598ce1c"), new Guid("7dbc7ce9-f34b-478f-9591-e2c190c402cc"), "Intro", 550, 270 });

            migrationBuilder.InsertData(
                table: "GraphNodes",
                columns: new[] { "Id", "GraphId", "Name", "X", "Y" },
                values: new object[] { new Guid("0475dbca-7b81-4a7d-8a86-fa45e38da9ba"), new Guid("7dbc7ce9-f34b-478f-9591-e2c190c402cc"), "Topic", 750, 370 });
        }
    }
}
