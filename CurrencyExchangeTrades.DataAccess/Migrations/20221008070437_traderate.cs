using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyExchangeTrades.DataAccess.Migrations
{
    public partial class traderate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Trades",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Trades");
        }
    }
}
