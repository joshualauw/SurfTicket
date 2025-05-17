using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfTicket.Migrations
{
    /// <inheritdoc />
    public partial class MerchantUserCompositeOptionalVenueLocationRemovePermissionAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionMenu_PermissionAdmin_PermissionAdminId",
                table: "PermissionMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_Venue_VenueLocation_VenueLocationId",
                table: "Venue");

            migrationBuilder.DropTable(
                name: "PermissionAdmin");

            migrationBuilder.DropIndex(
                name: "IX_PermissionMenu_PermissionAdminId",
                table: "PermissionMenu");

            migrationBuilder.DropIndex(
                name: "IX_MerchantUser_MerchantId",
                table: "MerchantUser");

            migrationBuilder.RenameColumn(
                name: "PermissionAdminId",
                table: "PermissionMenu",
                newName: "Code");

            migrationBuilder.AlterColumn<int>(
                name: "VenueLocationId",
                table: "Venue",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantUser_MerchantId_UserId",
                table: "MerchantUser",
                columns: new[] { "MerchantId", "UserId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Venue_VenueLocation_VenueLocationId",
                table: "Venue",
                column: "VenueLocationId",
                principalTable: "VenueLocation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venue_VenueLocation_VenueLocationId",
                table: "Venue");

            migrationBuilder.DropIndex(
                name: "IX_MerchantUser_MerchantId_UserId",
                table: "MerchantUser");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "PermissionMenu",
                newName: "PermissionAdminId");

            migrationBuilder.AlterColumn<int>(
                name: "VenueLocationId",
                table: "Venue",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PermissionAdmin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionAdmin", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionMenu_PermissionAdminId",
                table: "PermissionMenu",
                column: "PermissionAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantUser_MerchantId",
                table: "MerchantUser",
                column: "MerchantId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionMenu_PermissionAdmin_PermissionAdminId",
                table: "PermissionMenu",
                column: "PermissionAdminId",
                principalTable: "PermissionAdmin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venue_VenueLocation_VenueLocationId",
                table: "Venue",
                column: "VenueLocationId",
                principalTable: "VenueLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
