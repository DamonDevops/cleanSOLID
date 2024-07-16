using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_LeaveManagement.Identity.Migrations
{
    /// <inheritdoc />
    public partial class BaseServiceUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fc98501-ff86-4156-b938-275ae7518667",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0194f43d-f008-4884-a9c2-46d9b4a43ea8", "AQAAAAIAAYagAAAAECETJqsFRs6T16m5X0niqnpFKDoVJJ02V+V1boDSXUQmtwtRKwc7Whxy4WSjoj4D+g==", "1253bd42-3ad8-4e9b-994d-3d3fa346285f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d33faa83-1813-4c33-8230-49e284091b43",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0b14c69-7a6f-448a-84ac-0108cd327e77", "AQAAAAIAAYagAAAAEOIWOzi6EJso+1lgafv6SSLcUAiSUt9XFhdK6mqB3sWtvLKf+Gi10OnYfCjP1j1Pjg==", "677573e0-0160-4ce6-8c82-2efdd1272d49" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fc98501-ff86-4156-b938-275ae7518667",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2a43cce-19f2-4da8-9adf-29aa4c5455f0", "AQAAAAIAAYagAAAAEMbo3648LY+26PJ/fiv3mjFua42qKrLxLqAgbeYMJMROnUzTvhD4ychFwim6EkxJpQ==", "c92a626f-6dd5-466d-af02-c0c240224233" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d33faa83-1813-4c33-8230-49e284091b43",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63c4ccd4-6e19-4037-8b6c-2db5657e4440", "AQAAAAIAAYagAAAAEIFv5RwnmKkKQmkyqAcZE0MU6f6+satYwj9cFiREjzcOBa4svfemqozVYZA/62FTEw==", "ebaf9311-4a8d-4a83-9de5-27bf07a480ea" });
        }
    }
}
