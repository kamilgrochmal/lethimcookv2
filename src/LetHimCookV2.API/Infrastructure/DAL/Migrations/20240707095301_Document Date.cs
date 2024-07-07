using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetHimCookV2.API.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DocumentDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Patients");

            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentDate",
                table: "Documents",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentDate",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Patients",
                type: "text",
                nullable: true);
        }
    }
}
