using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class drugss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Cases_CaseId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_UserID",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "FileData",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Documents",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CaseId",
                table: "Documents",
                newName: "CaseID");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Documents",
                newName: "Url");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_UserID",
                table: "Documents",
                newName: "IX_Documents_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_CaseId",
                table: "Documents",
                newName: "IX_Documents_CaseID");

            migrationBuilder.RenameColumn(
                name: "CredentialValue",
                table: "Credentials",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "CredentialType",
                table: "Credentials",
                newName: "FileName");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Documents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CaseID",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Documents",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Credentials",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Cases_CaseID",
                table: "Documents",
                column: "CaseID",
                principalTable: "Cases",
                principalColumn: "CaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_UserId",
                table: "Documents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Cases_CaseID",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_UserId",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Credentials");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Documents",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "CaseID",
                table: "Documents",
                newName: "CaseId");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Documents",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_UserId",
                table: "Documents",
                newName: "IX_Documents_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_CaseID",
                table: "Documents",
                newName: "IX_Documents_CaseId");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Credentials",
                newName: "CredentialValue");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Credentials",
                newName: "CredentialType");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CaseId",
                table: "Documents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "DocumentId",
                table: "Documents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "Documents",
                type: "varbinary(max)",
                maxLength: 26214400,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Cases_CaseId",
                table: "Documents",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "CaseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_UserID",
                table: "Documents",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
