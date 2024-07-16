using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBaseEntityAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "leaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "leaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "leaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "leaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "leaveAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "leaveAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "DateCreated", "DateModified", "ModifiedBy" },
                values: new object[] { null, new DateTime(2024, 7, 16, 9, 53, 5, 193, DateTimeKind.Local).AddTicks(3807), new DateTime(2024, 7, 16, 9, 53, 5, 193, DateTimeKind.Local).AddTicks(3868), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "leaveTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "leaveTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "leaveAllocations");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "leaveAllocations");

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 15, 17, 3, 0, 502, DateTimeKind.Local).AddTicks(8098), new DateTime(2024, 7, 15, 17, 3, 0, 502, DateTimeKind.Local).AddTicks(8157) });
        }
    }
}
