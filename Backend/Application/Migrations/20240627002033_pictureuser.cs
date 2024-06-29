using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class pictureuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Picture",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Picture_UserId",
                table: "Picture",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Users_UserId",
                table: "Picture",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Users_UserId",
                table: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_Picture_UserId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Picture");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "Users",
                type: "varbinary(max)",
                maxLength: 26214400,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
