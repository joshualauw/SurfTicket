using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfTicket.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableNameFromTicketInvoiceToTicketPurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketEntry_TicketInvoice_TicketInvoiceId",
                table: "TicketEntry");

            migrationBuilder.DropTable(
                name: "TicketInvoice");

            migrationBuilder.RenameColumn(
                name: "TicketInvoiceId",
                table: "TicketEntry",
                newName: "TicketPurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketEntry_TicketInvoiceId",
                table: "TicketEntry",
                newName: "IX_TicketEntry_TicketPurchaseId");

            migrationBuilder.CreateTable(
                name: "TicketPurchase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketPurchaseId = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPurchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketPurchase_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketPurchase_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketPurchase_TicketId",
                table: "TicketPurchase",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPurchase_UserId",
                table: "TicketPurchase",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEntry_TicketPurchase_TicketPurchaseId",
                table: "TicketEntry",
                column: "TicketPurchaseId",
                principalTable: "TicketPurchase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketEntry_TicketPurchase_TicketPurchaseId",
                table: "TicketEntry");

            migrationBuilder.DropTable(
                name: "TicketPurchase");

            migrationBuilder.RenameColumn(
                name: "TicketPurchaseId",
                table: "TicketEntry",
                newName: "TicketInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketEntry_TicketPurchaseId",
                table: "TicketEntry",
                newName: "IX_TicketEntry_TicketInvoiceId");

            migrationBuilder.CreateTable(
                name: "TicketInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PurchasedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketInvoice_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketInvoice_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketInvoice_TicketId",
                table: "TicketInvoice",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketInvoice_UserId",
                table: "TicketInvoice",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEntry_TicketInvoice_TicketInvoiceId",
                table: "TicketEntry",
                column: "TicketInvoiceId",
                principalTable: "TicketInvoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
