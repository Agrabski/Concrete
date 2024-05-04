using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concrete.Core.Data.Migrations;

/// <inheritdoc />
public partial class AddExtensionDataStorage : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "ExtensionData",
			columns: table => new
			{
				Key = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
				ExtensionName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
				Category = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
				Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
			},
			constraints: table => table.PrimaryKey("PK_ExtensionData", x => new { x.Key, x.ExtensionName }));

		migrationBuilder.CreateIndex(
			name: "IX_ExtensionData_Category",
			table: "ExtensionData",
			column: "Category");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "ExtensionData");
	}
}
