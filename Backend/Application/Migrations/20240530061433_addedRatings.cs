using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addedRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvaliabilityDate",
                table: "Avaliabilities");

            migrationBuilder.DropColumn(
                name: "AvaliabilityTimeEnd",
                table: "Avaliabilities");

            migrationBuilder.DropColumn(
                name: "AvaliabilityTimeStart",
                table: "Avaliabilities");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Avaliabilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndHour",
                table: "Avaliabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartHour",
                table: "Avaliabilities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "DoctorRatings",
                columns: table => new
                {
                    DoctorRatingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorRatings", x => x.DoctorRatingID);
                    table.ForeignKey(
                        name: "FK_DoctorRatings_Users_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DoctorRatings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorRatings_DoctorID",
                table: "DoctorRatings",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorRatings_UserID",
                table: "DoctorRatings",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorRatings");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Avaliabilities");

            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "Avaliabilities");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "Avaliabilities");

            migrationBuilder.AddColumn<DateTime>(
                name: "AvaliabilityDate",
                table: "Avaliabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AvaliabilityTimeEnd",
                table: "Avaliabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AvaliabilityTimeStart",
                table: "Avaliabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
