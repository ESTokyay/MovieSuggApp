using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class mig_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "MovieEntity",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    page = table.Column<int>(type: "int", nullable: false),
                    total_results = table.Column<int>(type: "int", nullable: false),
                    total_pages = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieEntity", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adult = table.Column<bool>(type: "bit", nullable: false),
                    video = table.Column<bool>(type: "bit", nullable: false),
                    backdrop_path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id = table.Column<int>(type: "int", nullable: false),
                    vote_count = table.Column<int>(type: "int", nullable: false),
                    original_language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    release_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    original_title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    overview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    poster_path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vote_average = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    popularity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MovieEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_Film_MovieEntity_MovieEntityId",
                        column: x => x.MovieEntityId,
                        principalTable: "MovieEntity",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notlar",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icerik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    filmEntityId = table.Column<int>(type: "int", nullable: true),
                    kullaniciEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notlar", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_Notlar_Film_filmEntityId",
                        column: x => x.filmEntityId,
                        principalTable: "Film",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notlar_Kullanicilar_kullaniciEntityId",
                        column: x => x.kullaniciEntityId,
                        principalTable: "Kullanicilar",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Puanlar",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Puan = table.Column<int>(type: "int", nullable: false),
                    filmEntityId = table.Column<int>(type: "int", nullable: true),
                    kullaniciEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puanlar", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_Puanlar_Film_filmEntityId",
                        column: x => x.filmEntityId,
                        principalTable: "Film",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Puanlar_Kullanicilar_kullaniciEntityId",
                        column: x => x.kullaniciEntityId,
                        principalTable: "Kullanicilar",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Film_MovieEntityId",
                table: "Film",
                column: "MovieEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Notlar_filmEntityId",
                table: "Notlar",
                column: "filmEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Notlar_kullaniciEntityId",
                table: "Notlar",
                column: "kullaniciEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Puanlar_filmEntityId",
                table: "Puanlar",
                column: "filmEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Puanlar_kullaniciEntityId",
                table: "Puanlar",
                column: "kullaniciEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notlar");

            migrationBuilder.DropTable(
                name: "Puanlar");

            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "MovieEntity");
        }
    }
}
