using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanvasKpiLti.Migrations
{
    /// <inheritdoc />
    public partial class AddOutcomeTreeCanvas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutcomesCanvas",
                columns: table => new
                {
                    LmsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Architecture = table.Column<int>(type: "int", nullable: false),
                    Competence = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    LevelDivisorNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutcomesCanvas", x => x.LmsId);
                });

            migrationBuilder.CreateTable(
                name: "TreeRootCanvas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeRootCanvas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreeRootOutcome",
                columns: table => new
                {
                    OutcomesLmsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TreeRootId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeRootOutcome", x => new { x.OutcomesLmsId, x.TreeRootId });
                    table.ForeignKey(
                        name: "FK_TreeRootOutcome_OutcomesCanvas_OutcomesLmsId",
                        column: x => x.OutcomesLmsId,
                        principalTable: "OutcomesCanvas",
                        principalColumn: "LmsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreeRootOutcome_TreeRootCanvas_TreeRootId",
                        column: x => x.TreeRootId,
                        principalTable: "TreeRootCanvas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreeRootOutcome_TreeRootId",
                table: "TreeRootOutcome",
                column: "TreeRootId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreeRootOutcome");

            migrationBuilder.DropTable(
                name: "OutcomesCanvas");

            migrationBuilder.DropTable(
                name: "TreeRootCanvas");
        }
    }
}
