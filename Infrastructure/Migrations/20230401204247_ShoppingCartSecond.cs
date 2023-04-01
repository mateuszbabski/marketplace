using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCartSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Customers_CustomerId1",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_CustomerId1",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "ShoppingCarts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId1",
                table: "ShoppingCarts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_CustomerId1",
                table: "ShoppingCarts",
                column: "CustomerId1",
                unique: true,
                filter: "[CustomerId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Customers_CustomerId1",
                table: "ShoppingCarts",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
