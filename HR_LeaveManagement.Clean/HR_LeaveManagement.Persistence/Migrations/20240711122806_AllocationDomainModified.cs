using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AllocationDomainModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "leaveAllocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 11, 14, 28, 6, 147, DateTimeKind.Local).AddTicks(4729), new DateTime(2024, 7, 11, 14, 28, 6, 147, DateTimeKind.Local).AddTicks(4790) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "leaveAllocations");

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 28, 11, 19, 40, 751, DateTimeKind.Local).AddTicks(9154), new DateTime(2024, 6, 28, 11, 19, 40, 751, DateTimeKind.Local).AddTicks(9215) });
        }
    }
}
