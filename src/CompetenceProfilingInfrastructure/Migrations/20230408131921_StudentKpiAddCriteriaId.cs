﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanvasKpiLti.Migrations
{
    /// <inheritdoc />
    public partial class StudentKpiAddCriteriaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CriteriaId",
                table: "StudentKpi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriteriaId",
                table: "StudentKpi");
        }
    }
}
