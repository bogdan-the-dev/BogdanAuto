using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bogdan_Auto.Data.Migrations
{
    public partial class addIndividualPriceForProductItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ProductIndividualPrice",
                table: "OrderProduct",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductIndividualPrice",
                table: "OrderProduct");
        }
    }
}
