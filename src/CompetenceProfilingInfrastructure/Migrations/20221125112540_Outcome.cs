using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanvasKpiLti.Migrations
{
    /// <inheritdoc />
    public partial class Outcome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OutcomeId",
                table: "StudentAdvices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OutcomeId",
                table: "StudentAdvices");
        }
    }
}
