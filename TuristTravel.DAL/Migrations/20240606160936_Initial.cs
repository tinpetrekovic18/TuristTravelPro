using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuristTravel.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Destinacije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinacije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Hoteli",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cijenaNocenja = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoteli", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ponude",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinacijaID = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<int>(type: "int", nullable: false),
                    pocetakPutovanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    krajPutovanja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ponude", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ponude_Destinacije_DestinacijaID",
                        column: x => x.DestinacijaID,
                        principalTable: "Destinacije",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PonudaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Korisnici_Ponude_PonudaID",
                        column: x => x.PonudaID,
                        principalTable: "Ponude",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Putovanja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikID = table.Column<int>(type: "int", nullable: false),
                    PonudaID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Putovanja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Putovanja_Hoteli_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hoteli",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Putovanja_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Putovanja_Ponude_PonudaID",
                        column: x => x.PonudaID,
                        principalTable: "Ponude",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Destinacije",
                columns: new[] { "ID", "Naziv", "Opis" },
                values: new object[] { 1, "Zagreb", "Very nice" });

            migrationBuilder.InsertData(
                table: "Hoteli",
                columns: new[] { "ID", "Adresa", "Naziv", "cijenaNocenja" },
                values: new object[] { 1, "Zagreb", "Sheraton", 100 });

            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "ID", "Adresa", "Email", "Ime", "Password", "PonudaID", "Prezime" },
                values: new object[] { 1, "Pustodol Začretski 49a", "tin.petrekovic@gmail.com", "Tin", "lozinka123", null, "Petreković" });

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_PonudaID",
                table: "Korisnici",
                column: "PonudaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ponude_DestinacijaID",
                table: "Ponude",
                column: "DestinacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Putovanja_HotelID",
                table: "Putovanja",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Putovanja_KorisnikID",
                table: "Putovanja",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Putovanja_PonudaID",
                table: "Putovanja",
                column: "PonudaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Putovanja");

            migrationBuilder.DropTable(
                name: "Hoteli");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Ponude");

            migrationBuilder.DropTable(
                name: "Destinacije");
        }
    }
}
