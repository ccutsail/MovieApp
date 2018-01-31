using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MovieApp.Migrations
{
    public partial class added_filminfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "filminfo",
                columns: table => new
                {
                    Title = table.Column<string>(type: "varchar(127)", nullable: false),
                    Rating = table.Column<string>(type: "longtext", nullable: true),
                    ReleaseYear = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filminfo", x => x.Title);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "filminfo");
        }
    }
}
