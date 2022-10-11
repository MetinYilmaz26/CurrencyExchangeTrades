using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyExchangeTrades.DataAccess.Migrations
{
    public partial class columnsrequeried : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trades_CurrencySymbols_FromId",
                table: "Trades");

            migrationBuilder.DropForeignKey(
                name: "FK_Trades_CurrencySymbols_ToId",
                table: "Trades");

            migrationBuilder.AddForeignKey(
                name: "FK_Trades_CurrencySymbols_FromId",
                table: "Trades",
                column: "FromId",
                principalTable: "CurrencySymbols",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trades_CurrencySymbols_ToId",
                table: "Trades",
                column: "ToId",
                principalTable: "CurrencySymbols",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trades_CurrencySymbols_FromId",
                table: "Trades");

            migrationBuilder.DropForeignKey(
                name: "FK_Trades_CurrencySymbols_ToId",
                table: "Trades");

            migrationBuilder.AddForeignKey(
                name: "FK_Trades_CurrencySymbols_FromId",
                table: "Trades",
                column: "FromId",
                principalTable: "CurrencySymbols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trades_CurrencySymbols_ToId",
                table: "Trades",
                column: "ToId",
                principalTable: "CurrencySymbols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
