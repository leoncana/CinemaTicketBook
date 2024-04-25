using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.Migrations
{
    public partial class celebmovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCeleb_Celebs_CelebId",
                table: "MovieCeleb");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCeleb_Movies_MovieeId",
                table: "MovieCeleb");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCeleb_Movies_MovieId",
                table: "MovieCeleb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieCeleb",
                table: "MovieCeleb");

            migrationBuilder.DropIndex(
                name: "IX_MovieCeleb_MovieeId",
                table: "MovieCeleb");

            migrationBuilder.DropColumn(
                name: "MovieeId",
                table: "MovieCeleb");

            migrationBuilder.RenameTable(
                name: "MovieCeleb",
                newName: "MovieCelebs");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCeleb_CelebId",
                table: "MovieCelebs",
                newName: "IX_MovieCelebs_CelebId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieCelebs",
                table: "MovieCelebs",
                columns: new[] { "MovieId", "CelebId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCelebs_Celebs_CelebId",
                table: "MovieCelebs",
                column: "CelebId",
                principalTable: "Celebs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCelebs_Movies_MovieId",
                table: "MovieCelebs",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCelebs_Celebs_CelebId",
                table: "MovieCelebs");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCelebs_Movies_MovieId",
                table: "MovieCelebs");

            migrationBuilder.DropTable(
                name: "CelebMoviee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieCelebs",
                table: "MovieCelebs");

            migrationBuilder.RenameTable(
                name: "MovieCelebs",
                newName: "MovieCeleb");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCelebs_CelebId",
                table: "MovieCeleb",
                newName: "IX_MovieCeleb_CelebId");

            migrationBuilder.AddColumn<long>(
                name: "MovieeId",
                table: "MovieCeleb",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieCeleb",
                table: "MovieCeleb",
                columns: new[] { "MovieId", "CelebId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCeleb_MovieeId",
                table: "MovieCeleb",
                column: "MovieeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCeleb_Celebs_CelebId",
                table: "MovieCeleb",
                column: "CelebId",
                principalTable: "Celebs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCeleb_Movies_MovieeId",
                table: "MovieCeleb",
                column: "MovieeId",
                principalTable: "Movies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCeleb_Movies_MovieId",
                table: "MovieCeleb",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
