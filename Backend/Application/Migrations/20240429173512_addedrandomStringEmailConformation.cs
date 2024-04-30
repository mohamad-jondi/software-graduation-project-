using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addedrandomStringEmailConformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RandomStringEmailConfirmations",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandomStringEmailConfirmations",
                table: "Users");
        }
    }
}
