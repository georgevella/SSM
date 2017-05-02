using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteSpeedController.Master.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agents",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    created = table.Column<DateTime>(nullable: false),
                    hostIdentifier = table.Column<Guid>(nullable: false),
                    hostname = table.Column<string>(nullable: false),
                    isApproved = table.Column<bool>(nullable: false),
                    isDisabled = table.Column<bool>(nullable: false),
                    jobsRun = table.Column<int>(nullable: false),
                    lastUpdated = table.Column<DateTime>(nullable: false),
                    port = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "agentCountries",
                columns: table => new
                {
                    AgentId = table.Column<int>(nullable: false),
                    CountryId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agentCountries", x => new { x.AgentId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_agentCountries_agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "agents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_agentCountries_countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_agentCountries_CountryId",
                table: "agentCountries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_agents_hostIdentifier",
                table: "agents",
                column: "hostIdentifier",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agentCountries");

            migrationBuilder.DropTable(
                name: "agents");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
