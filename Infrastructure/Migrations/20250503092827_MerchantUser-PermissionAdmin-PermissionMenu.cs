using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfTicket.Migrations
{
    /// <inheritdoc />
    public partial class MerchantUserPermissionAdminPermissionMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MerchantUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MerchantUser_Merchant_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionAdmin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "PermissionMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionAdminId = table.Column<int>(type: "int", nullable: false),
                    MerchantUserId = table.Column<int>(type: "int", nullable: false),
                    Access = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionMenu_MerchantUser_MerchantUserId",
                        column: x => x.MerchantUserId,
                        principalTable: "MerchantUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionMenu_PermissionAdmin_PermissionAdminId",
                        column: x => x.PermissionAdminId,
                        principalTable: "PermissionAdmin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MerchantUser_MerchantId",
                table: "MerchantUser",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantUser_UserId",
                table: "MerchantUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionMenu_MerchantUserId",
                table: "PermissionMenu",
                column: "MerchantUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionMenu_PermissionAdminId",
                table: "PermissionMenu",
                column: "PermissionAdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionMenu");

            migrationBuilder.DropTable(
                name: "MerchantUser");

            migrationBuilder.DropTable(
                name: "PermissionAdmin");
        }
    }
}
