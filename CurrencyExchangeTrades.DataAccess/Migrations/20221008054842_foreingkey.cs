using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyExchangeTrades.DataAccess.Migrations
{
    public partial class foreingkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creater",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "Creater",
                table: "CurrencySymbols");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Creater",
                table: "Trades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Creater",
                table: "CurrencySymbols",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
