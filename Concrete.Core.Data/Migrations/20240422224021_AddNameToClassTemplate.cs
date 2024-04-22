﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concrete.Core.Data.Migrations;

/// <inheritdoc />
public partial class AddNameToClassTemplate : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<string>(
			name: "Name",
			table: "CLASS_TEMPLATE",
			type: "nvarchar(512)",
			maxLength: 512,
			nullable: false,
			defaultValue: "");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "Name",
			table: "CLASS_TEMPLATE");
	}
}
