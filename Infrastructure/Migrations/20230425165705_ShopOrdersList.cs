using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ShopOrdersList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopOrders_Orders_OrderId",
                table: "ShopOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopOrders_Orders_OrderId",
                table: "ShopOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopOrders_Orders_OrderId",
                table: "ShopOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopOrders_Orders_OrderId",
                table: "ShopOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
