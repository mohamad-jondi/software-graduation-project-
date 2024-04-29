using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddedJWTrefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JWTTokensRefresh",
                columns: table => new
                {
                    JWTTokensRefreshID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefreshToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWTTokensRefresh", x => x.JWTTokensRefreshID);
                    table.ForeignKey(
                        name: "FK_JWTTokensRefresh_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JWTTokensRefresh_UserID",
                table: "JWTTokensRefresh",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JWTTokensRefresh");
        }
    }
}
