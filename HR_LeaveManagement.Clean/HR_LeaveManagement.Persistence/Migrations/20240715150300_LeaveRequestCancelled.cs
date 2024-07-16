using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LeaveRequestCancelled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 15, 17, 3, 0, 502, DateTimeKind.Local).AddTicks(8098), new DateTime(2024, 7, 15, 17, 3, 0, 502, DateTimeKind.Local).AddTicks(8157) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 11, 14, 28, 6, 147, DateTimeKind.Local).AddTicks(4729), new DateTime(2024, 7, 11, 14, 28, 6, 147, DateTimeKind.Local).AddTicks(4790) });
        }
    }
}
