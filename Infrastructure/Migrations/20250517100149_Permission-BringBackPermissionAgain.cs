using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfTicket.Migrations
{
    /// <inheritdoc />
    public partial class PermissionBringBackPermissionAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "PermissionMenu");

            migrationBuilder.AddColumn<int>(
                name: "PermissionAdminId",
                table: "PermissionMenu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PermissionAdmin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionAdmin", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionMenu_PermissionAdminId",
                table: "PermissionMenu",
                column: "PermissionAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionMenu_PermissionAdmin_PermissionAdminId",
                table: "PermissionMenu",
                column: "PermissionAdminId",
                principalTable: "PermissionAdmin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionMenu_PermissionAdmin_PermissionAdminId",
                table: "PermissionMenu");

            migrationBuilder.DropTable(
                name: "PermissionAdmin");

            migrationBuilder.DropIndex(
                name: "IX_PermissionMenu_PermissionAdminId",
                table: "PermissionMenu");

            migrationBuilder.DropColumn(
                name: "PermissionAdminId",
                table: "PermissionMenu");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "PermissionMenu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
