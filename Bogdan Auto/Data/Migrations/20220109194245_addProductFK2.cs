using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bogdan_Auto.Data.Migrations
{
    public partial class addProductFK2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Manufacturers_CategoryIdFk",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoryIdFk",
                table: "Products",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryIdFk",
                table: "Products",
                newName: "IX_Products_Category");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Manufacturers_Category",
                table: "Products",
                column: "Category",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Manufacturers_Category",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Products",
                newName: "CategoryIdFk");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Category",
                table: "Products",
                newName: "IX_Products_CategoryIdFk");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Manufacturers_CategoryIdFk",
                table: "Products",
                column: "CategoryIdFk",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
