using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfTicket.Migrations
{
    /// <inheritdoc />
    public partial class VenueMerchantOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MerchantId",
                table: "Venue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Venue_MerchantId",
                table: "Venue",
                column: "MerchantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venue_Merchant_MerchantId",
                table: "Venue",
                column: "MerchantId",
                principalTable: "Merchant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venue_Merchant_MerchantId",
                table: "Venue");

            migrationBuilder.DropIndex(
                name: "IX_Venue_MerchantId",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "MerchantId",
                table: "Venue");
        }
    }
}
