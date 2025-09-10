using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContext.Migrations.SqlServerDbContext
{
    /// <inheritdoc />
    public partial class miInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "supusr");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "supusr",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "varchar(200)", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "supusr",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "varchar(200)", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "supusr",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(200)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(200)", nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "supusr",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "varchar(200)", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "supusr",
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "supusr",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "varchar(200)", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "supusr",
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sights",
                schema: "supusr",
                columns: table => new
                {
                    SightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SightName = table.Column<string>(type: "varchar(200)", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "varchar(200)", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sights", x => x.SightId);
                    table.ForeignKey(
                        name: "FK_Sights_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "supusr",
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                });

            migrationBuilder.CreateTable(
                name: "CategoryDbMSightDbM",
                schema: "supusr",
                columns: table => new
                {
                    SightDbMSightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDbMSightDbM", x => new { x.SightDbMSightId, x.SightId });
                    table.ForeignKey(
                        name: "FK_CategoryDbMSightDbM_Categories_SightId",
                        column: x => x.SightId,
                        principalSchema: "supusr",
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryDbMSightDbM_Sights_SightDbMSightId",
                        column: x => x.SightDbMSightId,
                        principalSchema: "supusr",
                        principalTable: "Sights",
                        principalColumn: "SightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "supusr",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "varchar(200)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Sights_SightId",
                        column: x => x.SightId,
                        principalSchema: "supusr",
                        principalTable: "Sights",
                        principalColumn: "SightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "supusr",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                schema: "supusr",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDbMSightDbM_SightId",
                schema: "supusr",
                table: "CategoryDbMSightDbM",
                column: "SightId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                schema: "supusr",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SightId",
                schema: "supusr",
                table: "Reviews",
                column: "SightId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                schema: "supusr",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sights_AddressId",
                schema: "supusr",
                table: "Sights",
                column: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryDbMSightDbM",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Sights",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "supusr");
        }
    }
}
