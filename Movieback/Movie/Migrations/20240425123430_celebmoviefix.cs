using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.Migrations
{
    public partial class celebmoviefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieCelebs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
