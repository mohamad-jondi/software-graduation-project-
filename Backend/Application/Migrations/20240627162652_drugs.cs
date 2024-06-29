using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class drugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchNumber",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "Contraindications",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "DosageForm",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "GenericName",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "Interactions",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "RefillInfo",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "RouteOfAdministration",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "SideEffects",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "StorageInstructions",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "Drug");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Drug",
                newName: "QuantityConsumed");

            migrationBuilder.AddColumn<int>(
                name: "DrugDosageTime",
                table: "Drug",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrugDosageTime",
                table: "Drug");

            migrationBuilder.RenameColumn(
                name: "QuantityConsumed",
                table: "Drug",
                newName: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "BatchNumber",
                table: "Drug",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "Drug",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contraindications",
                table: "Drug",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DosageForm",
                table: "Drug",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Drug",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Drug",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "Drug",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GenericName",
                table: "Drug",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Interactions",
                table: "Drug",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Drug",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RefillInfo",
                table: "Drug",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RouteOfAdministration",
                table: "Drug",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SideEffects",
                table: "Drug",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Drug",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageInstructions",
                table: "Drug",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Strength",
                table: "Drug",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
