using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksmartAPI.Migrations
{
    /// <inheritdoc />
    public partial class GenreProductManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_OrderProducts_OrderProductId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Products_ProductId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Authors_AuthorId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Categories_CategoryId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_AuthorId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_CategoryId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_Genres_OrderProductId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_ProductId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Pages",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "OrderProductId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Genres");

            migrationBuilder.CreateTable(
                name: "GenreProduct",
                columns: table => new
                {
                    GenresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreProduct", x => new { x.GenresId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_GenreProduct_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreProduct_ProductsId",
                table: "GenreProduct",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreProduct");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "OrderProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "OrderProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Pages",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderProductId",
                table: "Genres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Genres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_AuthorId",
                table: "OrderProducts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_CategoryId",
                table: "OrderProducts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_OrderProductId",
                table: "Genres",
                column: "OrderProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ProductId",
                table: "Genres",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_OrderProducts_OrderProductId",
                table: "Genres",
                column: "OrderProductId",
                principalTable: "OrderProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Products_ProductId",
                table: "Genres",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Authors_AuthorId",
                table: "OrderProducts",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Categories_CategoryId",
                table: "OrderProducts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
