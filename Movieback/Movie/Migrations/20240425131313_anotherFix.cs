using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.Migrations
{
    public partial class anotherFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CelebMoviee");

            migrationBuilder.CreateTable(
                name: "MovieCelebs",
                columns: table => new
                {
                    MovieId = table.Column<long>(type: "bigint", nullable: false),
                    CelebId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCelebs", x => new { x.MovieId, x.CelebId });
                    table.ForeignKey(
                        name: "FK_MovieCelebs_Celebs_CelebId",
                        column: x => x.CelebId,
                        principalTable: "Celebs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCelebs_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCelebs_CelebId",
                table: "MovieCelebs",
                column: "CelebId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieCelebs");

            migrationBuilder.CreateTable(
                name: "CelebMoviee",
                columns: table => new
                {
                    CelebId = table.Column<long>(type: "bigint", nullable: false),
                    MovieId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelebMoviee", x => new { x.CelebId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_CelebMoviee_Celebs_CelebId",
                        column: x => x.CelebId,
                        principalTable: "Celebs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CelebMoviee_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CelebMoviee_MovieId",
                table: "CelebMoviee",
                column: "MovieId");
        }
    }
}
