using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eLearning.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GraphEdges",
                keyColumn: "Id",
                keyValue: new Guid("56425c89-ec37-41bd-9b4b-54d23d4ee05c"));

            migrationBuilder.DeleteData(
                table: "GraphNodes",
                keyColumn: "Id",
                keyValue: new Guid("2368ef77-9c74-471a-baff-196fdb612944"));

            migrationBuilder.DeleteData(
                table: "GraphNodes",
                keyColumn: "Id",
                keyValue: new Guid("263fe5de-8282-44a4-8e3c-b39672ee015e"));

            migrationBuilder.DeleteData(
                table: "Graphs",
                keyColumn: "Id",
                keyValue: new Guid("c0720201-7227-4c57-8f5b-37d81ed0f9a2"));

            migrationBuilder.AddColumn<double>(
                name: "TranslateX",
                table: "Graphs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TranslateY",
                table: "Graphs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Graphs",
                columns: new[] { "Id", "Name", "Scale", "TranslateX", "TranslateY", "Type" },
                values: new object[] { new Guid("764e5e19-f77a-4d05-b497-b41c7f4e7495"), "Main graph", 1.0, 0.0, 0.0, 0 });

            migrationBuilder.InsertData(
                table: "GraphEdges",
                columns: new[] { "Id", "GraphId", "SourceNodeId", "TargetNodeId" },
                values: new object[] { new Guid("d4a279c6-0c33-4865-88fe-94b270fcdd76"), new Guid("764e5e19-f77a-4d05-b497-b41c7f4e7495"), new Guid("a364bcea-83f2-47b4-9ba0-aed9898137ed"), new Guid("41eec919-0922-4047-ac52-300b2813b59a") });

            migrationBuilder.InsertData(
                table: "GraphNodes",
                columns: new[] { "Id", "GraphId", "Name", "X", "Y" },
                values: new object[] { new Guid("a364bcea-83f2-47b4-9ba0-aed9898137ed"), new Guid("764e5e19-f77a-4d05-b497-b41c7f4e7495"), "Intro", 550, 270 });

            migrationBuilder.InsertData(
                table: "GraphNodes",
                columns: new[] { "Id", "GraphId", "Name", "X", "Y" },
                values: new object[] { new Guid("41eec919-0922-4047-ac52-300b2813b59a"), new Guid("764e5e19-f77a-4d05-b497-b41c7f4e7495"), "Topic", 750, 370 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GraphEdges",
                keyColumn: "Id",
                keyValue: new Guid("d4a279c6-0c33-4865-88fe-94b270fcdd76"));

            migrationBuilder.DeleteData(
                table: "GraphNodes",
                keyColumn: "Id",
                keyValue: new Guid("41eec919-0922-4047-ac52-300b2813b59a"));

            migrationBuilder.DeleteData(
                table: "GraphNodes",
                keyColumn: "Id",
                keyValue: new Guid("a364bcea-83f2-47b4-9ba0-aed9898137ed"));

            migrationBuilder.DeleteData(
                table: "Graphs",
                keyColumn: "Id",
                keyValue: new Guid("764e5e19-f77a-4d05-b497-b41c7f4e7495"));

            migrationBuilder.DropColumn(
                name: "TranslateX",
                table: "Graphs");

            migrationBuilder.DropColumn(
                name: "TranslateY",
                table: "Graphs");

            migrationBuilder.InsertData(
                table: "Graphs",
                columns: new[] { "Id", "Name", "Scale", "Type" },
                values: new object[] { new Guid("c0720201-7227-4c57-8f5b-37d81ed0f9a2"), "Main graph", 0.0, 0 });

            migrationBuilder.InsertData(
                table: "GraphEdges",
                columns: new[] { "Id", "GraphId", "SourceNodeId", "TargetNodeId" },
                values: new object[] { new Guid("56425c89-ec37-41bd-9b4b-54d23d4ee05c"), new Guid("c0720201-7227-4c57-8f5b-37d81ed0f9a2"), new Guid("263fe5de-8282-44a4-8e3c-b39672ee015e"), new Guid("2368ef77-9c74-471a-baff-196fdb612944") });

            migrationBuilder.InsertData(
                table: "GraphNodes",
                columns: new[] { "Id", "GraphId", "Name", "X", "Y" },
                values: new object[] { new Guid("263fe5de-8282-44a4-8e3c-b39672ee015e"), new Guid("c0720201-7227-4c57-8f5b-37d81ed0f9a2"), "Intro", 550, 270 });

            migrationBuilder.InsertData(
                table: "GraphNodes",
                columns: new[] { "Id", "GraphId", "Name", "X", "Y" },
                values: new object[] { new Guid("2368ef77-9c74-471a-baff-196fdb612944"), new Guid("c0720201-7227-4c57-8f5b-37d81ed0f9a2"), "Topic", 750, 370 });
        }
    }
}
