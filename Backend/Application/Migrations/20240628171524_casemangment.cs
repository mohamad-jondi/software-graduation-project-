using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class casemangment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Cases_CaseId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "CaseId",
                table: "Appointments",
                newName: "caseID");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CaseId",
                table: "Appointments",
                newName: "IX_Appointments_caseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Cases_caseID",
                table: "Appointments",
                column: "caseID",
                principalTable: "Cases",
                principalColumn: "CaseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Cases_caseID",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "caseID",
                table: "Appointments",
                newName: "CaseId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_caseID",
                table: "Appointments",
                newName: "IX_Appointments_CaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Cases_CaseId",
                table: "Appointments",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "CaseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
