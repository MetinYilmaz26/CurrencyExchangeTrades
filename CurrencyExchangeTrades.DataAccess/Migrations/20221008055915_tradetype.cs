using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyExchangeTrades.DataAccess.Migrations
{
    public partial class tradetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Trades",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Trades");
        }
    }
}
