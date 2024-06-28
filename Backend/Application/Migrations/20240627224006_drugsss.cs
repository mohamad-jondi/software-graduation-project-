using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class drugsss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isVerifedDoctor",
                table: "Users",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isVerifedDoctor",
                table: "Users");
        }
    }
}
