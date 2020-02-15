using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterConditionSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterConditionSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConditionAction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(maxLength: 250, nullable: true),
                    ActionParameterValues = table.Column<string>(nullable: true),
                    MasterConditionSetId = table.Column<int>(nullable: true),
                    CronExpression = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: true),
                    StopOnException = table.Column<bool>(nullable: true),
                    RetryCount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConditionActions_MasterConditionSet",
                        column: x => x.MasterConditionSetId,
                        principalTable: "MasterConditionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConditionSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterConditionISetd = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConditionSet_MasterConditionSet",
                        column: x => x.MasterConditionISetd,
                        principalTable: "MasterConditionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Condition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyName = table.Column<string>(maxLength: 250, nullable: true),
                    ConditionName = table.Column<string>(maxLength: 250, nullable: true),
                    ConditionValue = table.Column<string>(nullable: true),
                    ConditionPropertyValue = table.Column<string>(maxLength: 250, nullable: true),
                    ConditionSetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conditions_ConditionSet1",
                        column: x => x.ConditionSetId,
                        principalTable: "ConditionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Condition_ConditionSetId",
                table: "Condition",
                column: "ConditionSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionAction_MasterConditionSetId",
                table: "ConditionAction",
                column: "MasterConditionSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionSet_MasterConditionISetd",
                table: "ConditionSet",
                column: "MasterConditionISetd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.DropTable(
                name: "ConditionAction");

            migrationBuilder.DropTable(
                name: "ConditionSet");

            migrationBuilder.DropTable(
                name: "MasterConditionSet");
        }
    }
}
