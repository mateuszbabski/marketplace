using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Refactor2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "ShopOrders",
                newName: "ShippingAddress_Street");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "ShopOrders",
                newName: "ShippingAddress_PostalCode");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "ShopOrders",
                newName: "ShippingAddress_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "ShopOrders",
                newName: "ShippingAddress_City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Street",
                table: "ShopOrders",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_PostalCode",
                table: "ShopOrders",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Country",
                table: "ShopOrders",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_City",
                table: "ShopOrders",
                newName: "City");
        }
    }
}
