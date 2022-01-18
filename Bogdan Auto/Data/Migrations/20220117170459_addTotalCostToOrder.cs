using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bogdan_Auto.Data.Migrations
{
    public partial class addTotalCostToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalCost",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "Order");
        }
    }
}
