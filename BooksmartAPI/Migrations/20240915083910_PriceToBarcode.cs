using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksmartAPI.Migrations
{
    /// <inheritdoc />
    public partial class PriceToBarcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Barcodes",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "RentPrice",
                table: "Barcodes",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Barcodes");

            migrationBuilder.DropColumn(
                name: "RentPrice",
                table: "Barcodes");
        }
    }
}
