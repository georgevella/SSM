using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteSpeedManager.Master.Migrations
{
    public partial class Initial : Migration
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
                    isDisabled = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "datastores",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    hasCredentials = table.Column<bool>(nullable: false),
                    host = table.Column<string>(nullable: true),
                    isDefault = table.Column<bool>(nullable: false),
                    password = table.Column<string>(nullable: true),
                    port = table.Column<int>(nullable: false),
                    type = table.Column<int>(nullable: false),
                    username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datastores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "performanceProfiles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    downstream = table.Column<int>(nullable: false),
                    upstream = table.Column<int>(nullable: false),
                    speed = table.Column<int>(nullable: false),
                    speedIndexEnabled = table.Column<bool>(nullable: false),
                    useragent = table.Column<string>(nullable: true),
                    videoEnabled = table.Column<bool>(nullable: false),
                    viewportHeight = table.Column<int>(nullable: false),
                    viewportWidth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_performanceProfiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "agentCountries",
                columns: table => new
                {
                    agentId = table.Column<int>(nullable: false),
                    countryId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agentCountries", x => new { x.agentId, x.countryId });
                    table.ForeignKey(
                        name: "FK_agentCountries_agents_agentId",
                        column: x => x.agentId,
                        principalTable: "agents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_agentCountries_countries_countryId",
                        column: x => x.countryId,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sites",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    datastoreId = table.Column<int>(nullable: true),
                    domain = table.Column<string>(nullable: true),
                    isEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sites", x => x.id);
                    table.ForeignKey(
                        name: "FK_sites_datastores_datastoreId",
                        column: x => x.datastoreId,
                        principalTable: "datastores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    alias = table.Column<string>(nullable: true),
                    isEnabled = table.Column<bool>(nullable: false),
                    overridesCountryList = table.Column<bool>(nullable: false),
                    path = table.Column<string>(nullable: true),
                    siteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pages", x => x.id);
                    table.ForeignKey(
                        name: "FK_pages_sites_siteId",
                        column: x => x.siteId,
                        principalTable: "sites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "siteCountries",
                columns: table => new
                {
                    siteId = table.Column<int>(nullable: false),
                    countryId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_siteCountries", x => new { x.siteId, x.countryId });
                    table.ForeignKey(
                        name: "FK_siteCountries_countries_countryId",
                        column: x => x.countryId,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_siteCountries_sites_siteId",
                        column: x => x.siteId,
                        principalTable: "sites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "siteProfiles",
                columns: table => new
                {
                    siteId = table.Column<int>(nullable: false),
                    profileId = table.Column<string>(nullable: false),
                    ProfileId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_siteProfiles", x => new { x.siteId, x.profileId });
                    table.ForeignKey(
                        name: "FK_siteProfiles_performanceProfiles_ProfileId1",
                        column: x => x.ProfileId1,
                        principalTable: "performanceProfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_siteProfiles_sites_siteId",
                        column: x => x.siteId,
                        principalTable: "sites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pageCountries",
                columns: table => new
                {
                    pageId = table.Column<int>(nullable: false),
                    countryId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pageCountries", x => new { x.pageId, x.countryId });
                    table.ForeignKey(
                        name: "FK_pageCountries_countries_countryId",
                        column: x => x.countryId,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pageCountries_pages_pageId",
                        column: x => x.pageId,
                        principalTable: "pages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_agentCountries_countryId",
                table: "agentCountries",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_agents_hostIdentifier",
                table: "agents",
                column: "hostIdentifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pageCountries_countryId",
                table: "pageCountries",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_pages_siteId",
                table: "pages",
                column: "siteId");

            migrationBuilder.CreateIndex(
                name: "IX_siteCountries_countryId",
                table: "siteCountries",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_sites_datastoreId",
                table: "sites",
                column: "datastoreId");

            migrationBuilder.CreateIndex(
                name: "IX_siteProfiles_ProfileId1",
                table: "siteProfiles",
                column: "ProfileId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agentCountries");

            migrationBuilder.DropTable(
                name: "pageCountries");

            migrationBuilder.DropTable(
                name: "siteCountries");

            migrationBuilder.DropTable(
                name: "siteProfiles");

            migrationBuilder.DropTable(
                name: "agents");

            migrationBuilder.DropTable(
                name: "pages");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "performanceProfiles");

            migrationBuilder.DropTable(
                name: "sites");

            migrationBuilder.DropTable(
                name: "datastores");
        }
    }
}
