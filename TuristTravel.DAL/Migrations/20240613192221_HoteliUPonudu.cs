using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuristTravel.DAL.Migrations
{
    /// <inheritdoc />
    public partial class HoteliUPonudu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnici_Ponude_PonudaID",
                table: "Korisnici");

            migrationBuilder.DropForeignKey(
                name: "FK_Putovanja_Hoteli_HotelID",
                table: "Putovanja");

            migrationBuilder.DropIndex(
                name: "IX_Korisnici_PonudaID",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "PonudaID",
                table: "Korisnici");

            migrationBuilder.AlterColumn<int>(
                name: "HotelID",
                table: "Putovanja",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "HotelID",
                table: "Ponude",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ponude_HotelID",
                table: "Ponude",
                column: "HotelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ponude_Hoteli_HotelID",
                table: "Ponude",
                column: "HotelID",
                principalTable: "Hoteli",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Putovanja_Hoteli_HotelID",
                table: "Putovanja",
                column: "HotelID",
                principalTable: "Hoteli",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ponude_Hoteli_HotelID",
                table: "Ponude");

            migrationBuilder.DropForeignKey(
                name: "FK_Putovanja_Hoteli_HotelID",
                table: "Putovanja");

            migrationBuilder.DropIndex(
                name: "IX_Ponude_HotelID",
                table: "Ponude");

            migrationBuilder.DropColumn(
                name: "HotelID",
                table: "Ponude");

            migrationBuilder.AlterColumn<int>(
                name: "HotelID",
                table: "Putovanja",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PonudaID",
                table: "Korisnici",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "ID",
                keyValue: 1,
                column: "PonudaID",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_PonudaID",
                table: "Korisnici",
                column: "PonudaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnici_Ponude_PonudaID",
                table: "Korisnici",
                column: "PonudaID",
                principalTable: "Ponude",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Putovanja_Hoteli_HotelID",
                table: "Putovanja",
                column: "HotelID",
                principalTable: "Hoteli",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
