using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concrete.Core.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddModifiedToExtensionData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "ExtensionData",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ExtensionData_Modified",
                table: "ExtensionData",
                column: "Modified");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExtensionData_Modified",
                table: "ExtensionData");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "ExtensionData");
        }
    }
}
