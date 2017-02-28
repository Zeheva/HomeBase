using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomeBase.Migrations
{
    public partial class MaxLengthOnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeamName",
                table: "Team",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CaptainID",
                table: "Team",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Player",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Player",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Captain",
                columns: table => new
                {
                    CaptainID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Captain", x => x.CaptainID);
                });

            migrationBuilder.CreateTable(
                name: "TeamAssignment",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false),
                    CaptainID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamAssignment", x => new { x.TeamID, x.CaptainID });
                    table.ForeignKey(
                        name: "FK_TeamAssignment_Captain_CaptainID",
                        column: x => x.CaptainID,
                        principalTable: "Captain",
                        principalColumn: "CaptainID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamAssignment_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Team_CaptainID",
                table: "Team",
                column: "CaptainID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamAssignment_CaptainID",
                table: "TeamAssignment",
                column: "CaptainID");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Captain_CaptainID",
                table: "Team",
                column: "CaptainID",
                principalTable: "Captain",
                principalColumn: "CaptainID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Captain_CaptainID",
                table: "Team");

            migrationBuilder.DropTable(
                name: "TeamAssignment");

            migrationBuilder.DropTable(
                name: "Captain");

            migrationBuilder.DropIndex(
                name: "IX_Team_CaptainID",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "CaptainID",
                table: "Team");

            migrationBuilder.AlterColumn<string>(
                name: "TeamName",
                table: "Team",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Player",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Player",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
