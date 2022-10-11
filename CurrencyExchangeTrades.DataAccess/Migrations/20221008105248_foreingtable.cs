using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyExchangeTrades.DataAccess.Migrations
{
    public partial class foreingtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "To",
                table: "Trades",
                newName: "ToId");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "Trades",
                newName: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_FromId",
                table: "Trades",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_ToId",
                table: "Trades",
                column: "ToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trades_CurrencySymbols_FromId",
                table: "Trades",
                column: "FromId",
                principalTable: "CurrencySymbols",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Trades_CurrencySymbols_ToId",
                table: "Trades",
                column: "ToId",
                principalTable: "CurrencySymbols",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trades_CurrencySymbols_FromId",
                table: "Trades");

            migrationBuilder.DropForeignKey(
                name: "FK_Trades_CurrencySymbols_ToId",
                table: "Trades");

            migrationBuilder.DropIndex(
                name: "IX_Trades_FromId",
                table: "Trades");

            migrationBuilder.DropIndex(
                name: "IX_Trades_ToId",
                table: "Trades");

            migrationBuilder.RenameColumn(
                name: "ToId",
                table: "Trades",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "FromId",
                table: "Trades",
                newName: "From");
        }
    }
}
