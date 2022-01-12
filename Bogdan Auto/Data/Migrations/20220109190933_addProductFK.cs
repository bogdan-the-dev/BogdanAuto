using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bogdan_Auto.Data.Migrations
{
    public partial class addProductFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "categoryId");

            migrationBuilder.RenameColumn(
                name: "ManufacturerId",
                table: "Products",
                newName: "CategoryIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Products_categoryId",
                table: "Products",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryIdFk",
                table: "Products",
                column: "CategoryIdFk");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_categoryId",
                table: "Products",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Manufacturers_CategoryIdFk",
                table: "Products",
                column: "CategoryIdFk",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_categoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Manufacturers_CategoryIdFk",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_categoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryIdFk",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryIdFk",
                table: "Products",
                newName: "ManufacturerId");
        }
    }
}
