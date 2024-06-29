using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class drugslist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drug_TreatmentPlan_TreatmentPlanID",
                table: "Drug");

            migrationBuilder.DropForeignKey(
                name: "FK_Drug_Users_PatientID",
                table: "Drug");

            migrationBuilder.DropForeignKey(
                name: "FK_Drug_Users_PrescribedByID",
                table: "Drug");

            migrationBuilder.DropTable(
                name: "TreatmentPlan");

            migrationBuilder.DropIndex(
                name: "IX_Drug_PatientID",
                table: "Drug");

            migrationBuilder.DropIndex(
                name: "IX_Drug_TreatmentPlanID",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "PatientID",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "TreatmentPlanID",
                table: "Drug");

            migrationBuilder.RenameColumn(
                name: "PrescribedByID",
                table: "Drug",
                newName: "CaseID");

            migrationBuilder.RenameIndex(
                name: "IX_Drug_PrescribedByID",
                table: "Drug",
                newName: "IX_Drug_CaseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Drug_Cases_CaseID",
                table: "Drug",
                column: "CaseID",
                principalTable: "Cases",
                principalColumn: "CaseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drug_Cases_CaseID",
                table: "Drug");

            migrationBuilder.RenameColumn(
                name: "CaseID",
                table: "Drug",
                newName: "PrescribedByID");

            migrationBuilder.RenameIndex(
                name: "IX_Drug_CaseID",
                table: "Drug",
                newName: "IX_Drug_PrescribedByID");

            migrationBuilder.AddColumn<int>(
                name: "PatientID",
                table: "Drug",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TreatmentPlanID",
                table: "Drug",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TreatmentPlan",
                columns: table => new
                {
                    TreatmentPlanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    DrugUsage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentPlan", x => x.TreatmentPlanID);
                    table.ForeignKey(
                        name: "FK_TreatmentPlan_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "CaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drug_PatientID",
                table: "Drug",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Drug_TreatmentPlanID",
                table: "Drug",
                column: "TreatmentPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPlan_CaseID",
                table: "TreatmentPlan",
                column: "CaseID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Drug_TreatmentPlan_TreatmentPlanID",
                table: "Drug",
                column: "TreatmentPlanID",
                principalTable: "TreatmentPlan",
                principalColumn: "TreatmentPlanID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drug_Users_PatientID",
                table: "Drug",
                column: "PatientID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drug_Users_PrescribedByID",
                table: "Drug",
                column: "PrescribedByID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
