using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Celebs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CelebType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CelebName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CelebRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CelebImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Celebs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PortraitImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandscapeImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreenType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieCeleb",
                columns: table => new
                {
                    MovieId = table.Column<long>(type: "bigint", nullable: false),
                    CelebId = table.Column<long>(type: "bigint", nullable: false),
                    MovieeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCeleb", x => new { x.MovieId, x.CelebId });
                    table.ForeignKey(
                        name: "FK_MovieCeleb_Celebs_CelebId",
                        column: x => x.CelebId,
                        principalTable: "Celebs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCeleb_Movies_MovieeId",
                        column: x => x.MovieeId,
                        principalTable: "Movies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovieCeleb_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieSchedules",
                columns: table => new
                {
                    MovieScheduleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovieId = table.Column<long>(type: "bigint", nullable: false),
                    ScreenId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieSchedules", x => x.MovieScheduleId);
                    table.ForeignKey(
                        name: "FK_MovieSchedules_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieSchedules_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovieId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreenId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Col = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    BookingId = table.Column<long>(type: "bigint", nullable: false),
                    MovieScheduleId = table.Column<long>(type: "bigint", nullable: true),
                    ScreenId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK_Seats_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seats_MovieSchedules_MovieScheduleId",
                        column: x => x.MovieScheduleId,
                        principalTable: "MovieSchedules",
                        principalColumn: "MovieScheduleId");
                    table.ForeignKey(
                        name: "FK_Seats_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCeleb_CelebId",
                table: "MovieCeleb",
                column: "CelebId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCeleb_MovieeId",
                table: "MovieCeleb",
                column: "MovieeId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieSchedules_MovieId",
                table: "MovieSchedules",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieSchedules_ScreenId",
                table: "MovieSchedules",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_BookingId",
                table: "Seats",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_MovieScheduleId",
                table: "Seats",
                column: "MovieScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_ScreenId",
                table: "Seats",
                column: "ScreenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "MovieCeleb");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Celebs");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "MovieSchedules");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Screens");
        }
    }
}
