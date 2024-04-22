using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concrete.Core.Data.Migrations;

/// <inheritdoc />
public partial class InitialWithTemplates : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "COURSE_TEMPLATE",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
			},
			constraints: table => table.PrimaryKey("PK_COURSE_TEMPLATE", x => x.Id));

		migrationBuilder.CreateTable(
			name: "CLASS_TEMPLATE",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				CourseTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_CLASS_TEMPLATE", x => x.Id);
				table.ForeignKey(
					name: "FK_CLASS_TEMPLATE_COURSE_TEMPLATE_CourseTemplateId",
					column: x => x.CourseTemplateId,
					principalTable: "COURSE_TEMPLATE",
					principalColumn: "Id");
			});

		migrationBuilder.CreateTable(
			name: "ACTIVITY_TEMPLATE",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
				ClassTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
				DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
				TemplateData = table.Column<string>(type: "nvarchar(max)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_ACTIVITY_TEMPLATE", x => x.Id);
				table.ForeignKey(
					name: "FK_ACTIVITY_TEMPLATE_CLASS_TEMPLATE_ClassTemplateId",
					column: x => x.ClassTemplateId,
					principalTable: "CLASS_TEMPLATE",
					principalColumn: "Id");
			});

		migrationBuilder.CreateIndex(
			name: "IX_ACTIVITY_TEMPLATE_ClassTemplateId",
			table: "ACTIVITY_TEMPLATE",
			column: "ClassTemplateId");

		migrationBuilder.CreateIndex(
			name: "IX_CLASS_TEMPLATE_CourseTemplateId",
			table: "CLASS_TEMPLATE",
			column: "CourseTemplateId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "ACTIVITY_TEMPLATE");

		migrationBuilder.DropTable(
			name: "CLASS_TEMPLATE");

		migrationBuilder.DropTable(
			name: "COURSE_TEMPLATE");
	}
}
