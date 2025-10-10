using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExamSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddVwSubmissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view VwSubmissions
as
SELECT        dbo.TbSubmissions.SubmissionId, dbo.TbSubmissions.UserId, dbo.TbSubmissions.ExamId, dbo.TbSubmissions.Score, dbo.TbSubmissions.Status, dbo.TbSubmissions.SubmissionDate, dbo.TbSubmissions.CurrentState, 
                         dbo.TbExams.ExamTitle, dbo.AspNetUsers.FirstName, dbo.AspNetUsers.LastName
FROM            dbo.AspNetUsers INNER JOIN
                         dbo.TbSubmissions ON dbo.AspNetUsers.Id = dbo.TbSubmissions.UserId INNER JOIN
                         dbo.TbExams ON dbo.TbSubmissions.ExamId = dbo.TbExams.ExamId");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TbQuestions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TbQuestions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "TbQuestions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TbQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TbQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "TbQuestions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
