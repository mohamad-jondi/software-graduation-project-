using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class madeChatsBetweenUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_DoctorID",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_PatientID",
                table: "Chats");

            migrationBuilder.DropTable(
                name: "DoctorPatient");

            migrationBuilder.DropIndex(
                name: "IX_LifestyleFactors_PatientId",
                table: "LifestyleFactors");

            migrationBuilder.DropColumn(
                name: "Sender",
                table: "ChatMessages");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "Chats",
                newName: "SecondPartyID");

            migrationBuilder.RenameColumn(
                name: "DoctorID",
                table: "Chats",
                newName: "FirstPartyID");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_PatientID",
                table: "Chats",
                newName: "IX_Chats_SecondPartyID");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_DoctorID",
                table: "Chats",
                newName: "IX_Chats_FirstPartyID");

            // Add a temporary column for ProfilePicture
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePictureTemp",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);

            // Copy data from the old ProfilePicture column to the new temporary column
            migrationBuilder.Sql("UPDATE Users SET ProfilePictureTemp = CONVERT(varbinary(max), ProfilePicture)");

            // Drop the old ProfilePicture column
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Users");

            // Rename the temporary column to the original column name
            migrationBuilder.RenameColumn(
                name: "ProfilePictureTemp",
                table: "Users",
                newName: "ProfilePicture");

            // Set max length for ProfilePicture
            migrationBuilder.AlterColumn<byte[]>(
                name: "ProfilePicture",
                table: "Users",
                type: "varbinary(max)",
                maxLength: 26214400,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<int>(
                name: "CaseId",
                table: "Operations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Chats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ChatMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "ChatMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SenderUsername",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    CaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    NurseID = table.Column<int>(type: "int", nullable: false),
                    CaseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TreatmentPlan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextAppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.CaseId);
                    table.ForeignKey(
                        name: "FK_Cases_Users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cases_Users_NurseID",
                        column: x => x.NurseID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cases_Users_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "CaseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Users_UserId",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalSecondOpinions",
                columns: table => new
                {
                    SecondOpinionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    ReviewingDoctorId = table.Column<int>(type: "int", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SecondOpinionDiagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalSecondOpinions", x => x.SecondOpinionId);
                    table.ForeignKey(
                        name: "FK_MedicalSecondOpinions_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "CaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalSecondOpinions_Users_ReviewingDoctorId",
                        column: x => x.ReviewingDoctorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_CaseId",
                table: "Operations",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_LifestyleFactors_PatientId",
                table: "LifestyleFactors",
                column: "PatientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_DoctorId",
                table: "Cases",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_NurseID",
                table: "Cases",
                column: "NurseID");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_PatientID",
                table: "Cases",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CaseId",
                table: "Documents",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserID",
                table: "Documents",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalSecondOpinions_CaseId",
                table: "MedicalSecondOpinions",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalSecondOpinions_ReviewingDoctorId",
                table: "MedicalSecondOpinions",
                column: "ReviewingDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_FirstPartyID",
                table: "Chats",
                column: "FirstPartyID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_SecondPartyID",
                table: "Chats",
                column: "SecondPartyID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Cases_CaseId",
                table: "Operations",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "CaseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_FirstPartyID",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_SecondPartyID",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Cases_CaseId",
                table: "Operations");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "MedicalSecondOpinions");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Operations_CaseId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_LifestyleFactors_PatientId",
                table: "LifestyleFactors");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "CaseId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "SenderUsername",
                table: "ChatMessages");

            migrationBuilder.RenameColumn(
                name: "SecondPartyID",
                table: "Chats",
                newName: "PatientID");

            migrationBuilder.RenameColumn(
                name: "FirstPartyID",
                table: "Chats",
                newName: "DoctorID");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_SecondPartyID",
                table: "Chats",
                newName: "IX_Chats_PatientID");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_FirstPartyID",
                table: "Chats",
                newName: "IX_Chats_DoctorID");

            // Add the old ProfilePicture column
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // Copy data from the new ProfilePicture column to the old one
            migrationBuilder.Sql("UPDATE Users SET ProfilePicture = CONVERT(nvarchar(max), ProfilePictureTemp)");

            // Drop the new ProfilePictureTemp column
            migrationBuilder.DropColumn(
                name: "ProfilePictureTemp",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Sender",
                table: "ChatMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DoctorPatient",
                columns: table => new
                {
                    DoctorsId = table.Column<int>(type: "int", nullable: false),
                    PatientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorPatient", x => new { x.DoctorsId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_DoctorPatient_Users_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorPatient_Users_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LifestyleFactors_PatientId",
                table: "LifestyleFactors",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatient_PatientsId",
                table: "DoctorPatient",
                column: "PatientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_DoctorID",
                table: "Chats",
                column: "DoctorID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_PatientID",
                table: "Chats",
                column: "PatientID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
