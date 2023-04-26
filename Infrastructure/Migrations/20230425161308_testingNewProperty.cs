using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testingNewProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "ShopOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShopOrders_OrderId",
                table: "ShopOrders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopOrders_Orders_OrderId",
                table: "ShopOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopOrders_Orders_OrderId",
                table: "ShopOrders");

            migrationBuilder.DropIndex(
                name: "IX_ShopOrders_OrderId",
                table: "ShopOrders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ShopOrders");
        }
    }
}
