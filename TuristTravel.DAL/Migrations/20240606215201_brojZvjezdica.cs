using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuristTravel.DAL.Migrations
{
    /// <inheritdoc />
    public partial class brojZvjezdica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "brojZvjezdica",
                table: "Hoteli",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Hoteli",
                keyColumn: "ID",
                keyValue: 1,
                column: "brojZvjezdica",
                value: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "brojZvjezdica",
                table: "Hoteli");
        }
    }
}
