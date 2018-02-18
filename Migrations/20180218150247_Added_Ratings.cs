using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MovieApp.Migrations
{
    public partial class Added_Ratings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_film_Rating",
                table: "film");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "film");

            migrationBuilder.AddColumn<string>(
                name: "RatingCode",
                table: "film",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatingID",
                table: "film",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_film_RatingCode",
                table: "film",
                column: "RatingCode");

            migrationBuilder.CreateIndex(
                name: "IX_film_RatingID",
                table: "film",
                column: "RatingID");

            migrationBuilder.AddForeignKey(
                name: "FK_film_Rating_RatingID",
                table: "film",
                column: "RatingID",
                principalTable: "Rating",
                principalColumn: "RatingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_film_Rating_RatingID",
                table: "film");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_film_RatingCode",
                table: "film");

            migrationBuilder.DropIndex(
                name: "IX_film_RatingID",
                table: "film");

            migrationBuilder.DropColumn(
                name: "RatingCode",
                table: "film");

            migrationBuilder.DropColumn(
                name: "RatingID",
                table: "film");

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "film",
                maxLength: 45,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_film_Rating",
                table: "film",
                column: "Rating");
        }
    }
}
