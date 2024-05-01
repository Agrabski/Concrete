using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concrete.Core.Data.Migrations;

/// <inheritdoc />
public partial class UpdateJsonHandling : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.RenameColumn(
			name: "TypeName",
			table: "ACTIVITY_TEMPLATE",
			newName: "Discriminator");

		migrationBuilder.RenameColumn(
			name: "TemplateData",
			table: "ACTIVITY_TEMPLATE",
			newName: "Data");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.RenameColumn(
			name: "Discriminator",
			table: "ACTIVITY_TEMPLATE",
			newName: "TypeName");

		migrationBuilder.RenameColumn(
			name: "Data",
			table: "ACTIVITY_TEMPLATE",
			newName: "TemplateData");
	}
}
