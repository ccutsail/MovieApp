using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MovieApp.Migrations
{
    public partial class added_filminfo_CompKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_filminfo",
                table: "filminfo");

            migrationBuilder.AlterColumn<string>(
                name: "ReleaseYear",
                table: "filminfo",
                type: "varchar(127)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_filminfo",
                table: "filminfo",
                columns: new[] { "Title", "ReleaseYear" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_filminfo",
                table: "filminfo");

            migrationBuilder.AlterColumn<string>(
                name: "ReleaseYear",
                table: "filminfo",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(127)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_filminfo",
                table: "filminfo",
                column: "Title");
        }
    }
}
