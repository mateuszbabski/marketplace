﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230504181545_statusChange4")]
    partial class statusChange4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Customers.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int")
                        .HasColumnName("Role");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("Domain.Customers.Entities.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int")
                        .HasColumnName("OrderStatus");

                    b.Property<DateTime>("PlacedOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("PlacedOn");

                    b.Property<DateTime?>("StatusChanged")
                        .HasColumnType("datetime2")
                        .HasColumnName("StatusChanged");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Domain.Customers.Entities.Orders.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", (string)null);
                });

            modelBuilder.Entity("Domain.Customers.Entities.ShoppingCarts.ShoppingCart", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique()
                        .HasFilter("[CustomerId] IS NOT NULL");

                    b.ToTable("ShoppingCarts", (string)null);
                });

            modelBuilder.Entity("Domain.Customers.Entities.ShoppingCarts.ShoppingCartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantity");

                    b.Property<Guid?>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ShoppingCartId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartItems", (string)null);
                });

            modelBuilder.Entity("Domain.Shops.Entities.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Domain.Shops.Entities.ShopOrders.ShopOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int")
                        .HasColumnName("ShopOrderStatus");

                    b.Property<DateTime>("PlacedOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("PlacedOn");

                    b.Property<Guid?>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("StatusChanged")
                        .HasColumnType("datetime2")
                        .HasColumnName("StatusChanged");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ShopId");

                    b.ToTable("ShopOrders", (string)null);
                });

            modelBuilder.Entity("Domain.Shops.Entities.ShopOrders.ShopOrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantity");

                    b.Property<Guid?>("ShopOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShopOrderId");

                    b.ToTable("ShopOrderItems", (string)null);
                });

            modelBuilder.Entity("Domain.Shops.Shop", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int")
                        .HasColumnName("Role");

                    b.Property<string>("ShopName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Shops", (string)null);
                });

            modelBuilder.Entity("Domain.Customers.Customer", b =>
                {
                    b.OwnsOne("Domain.Shared.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Country");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("PostalCode");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Street");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Domain.Customers.Entities.Orders.Order", b =>
                {
                    b.HasOne("Domain.Customers.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Domain.Shared.ValueObjects.Address", "ShippingAddress", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Country");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("PostalCode");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Street");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("Domain.Shared.ValueObjects.MoneyValue", "TotalPrice", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Currency");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Customer");

                    b.Navigation("ShippingAddress");

                    b.Navigation("TotalPrice");
                });

            modelBuilder.Entity("Domain.Customers.Entities.Orders.OrderItem", b =>
                {
                    b.HasOne("Domain.Customers.Entities.Orders.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Domain.Shared.ValueObjects.MoneyValue", "Price", b1 =>
                        {
                            b1.Property<Guid>("OrderItemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Currency");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("OrderItems");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.Navigation("Order");

                    b.Navigation("Price");
                });

            modelBuilder.Entity("Domain.Customers.Entities.ShoppingCarts.ShoppingCart", b =>
                {
                    b.HasOne("Domain.Customers.Customer", null)
                        .WithOne("ShoppingCart")
                        .HasForeignKey("Domain.Customers.Entities.ShoppingCarts.ShoppingCart", "CustomerId");

                    b.OwnsOne("Domain.Shared.ValueObjects.MoneyValue", "TotalPrice", b1 =>
                        {
                            b1.Property<Guid>("ShoppingCartId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Currency");

                            b1.HasKey("ShoppingCartId");

                            b1.ToTable("ShoppingCarts");

                            b1.WithOwner()
                                .HasForeignKey("ShoppingCartId");
                        });

                    b.Navigation("TotalPrice");
                });

            modelBuilder.Entity("Domain.Customers.Entities.ShoppingCarts.ShoppingCartItem", b =>
                {
                    b.HasOne("Domain.Customers.Entities.ShoppingCarts.ShoppingCart", "ShoppingCart")
                        .WithMany("Items")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Domain.Shared.ValueObjects.MoneyValue", "Price", b1 =>
                        {
                            b1.Property<Guid>("ShoppingCartItemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Currency");

                            b1.HasKey("ShoppingCartItemId");

                            b1.ToTable("ShoppingCartItems");

                            b1.WithOwner()
                                .HasForeignKey("ShoppingCartItemId");
                        });

                    b.Navigation("Price");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("Domain.Shops.Entities.Products.Product", b =>
                {
                    b.HasOne("Domain.Shops.Shop", "Shop")
                        .WithMany("ProductList")
                        .HasForeignKey("ShopId");

                    b.OwnsOne("Domain.Shared.ValueObjects.MoneyValue", "Price", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Currency");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Price");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Domain.Shops.Entities.ShopOrders.ShopOrder", b =>
                {
                    b.HasOne("Domain.Customers.Entities.Orders.Order", "Order")
                        .WithMany("ShopOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Shops.Shop", "Shop")
                        .WithMany("ShopOrdersList")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Domain.Shared.ValueObjects.Address", "ShippingAddress", b1 =>
                        {
                            b1.Property<Guid>("ShopOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Country");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("PostalCode");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Street");

                            b1.HasKey("ShopOrderId");

                            b1.ToTable("ShopOrders");

                            b1.WithOwner()
                                .HasForeignKey("ShopOrderId");
                        });

                    b.OwnsOne("Domain.Shared.ValueObjects.MoneyValue", "TotalPrice", b1 =>
                        {
                            b1.Property<Guid>("ShopOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Currency");

                            b1.HasKey("ShopOrderId");

                            b1.ToTable("ShopOrders");

                            b1.WithOwner()
                                .HasForeignKey("ShopOrderId");
                        });

                    b.Navigation("Order");

                    b.Navigation("ShippingAddress");

                    b.Navigation("Shop");

                    b.Navigation("TotalPrice");
                });

            modelBuilder.Entity("Domain.Shops.Entities.ShopOrders.ShopOrderItem", b =>
                {
                    b.HasOne("Domain.Shops.Entities.ShopOrders.ShopOrder", "ShopOrder")
                        .WithMany("ShopOrderItems")
                        .HasForeignKey("ShopOrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Domain.Shared.ValueObjects.MoneyValue", "Price", b1 =>
                        {
                            b1.Property<Guid>("ShopOrderItemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Currency");

                            b1.HasKey("ShopOrderItemId");

                            b1.ToTable("ShopOrderItems");

                            b1.WithOwner()
                                .HasForeignKey("ShopOrderItemId");
                        });

                    b.Navigation("Price");

                    b.Navigation("ShopOrder");
                });

            modelBuilder.Entity("Domain.Shops.Shop", b =>
                {
                    b.OwnsOne("Domain.Shared.ValueObjects.Address", "ShopAddress", b1 =>
                        {
                            b1.Property<Guid>("ShopId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Country");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("PostalCode");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Street");

                            b1.HasKey("ShopId");

                            b1.ToTable("Shops");

                            b1.WithOwner()
                                .HasForeignKey("ShopId");
                        });

                    b.Navigation("ShopAddress");
                });

            modelBuilder.Entity("Domain.Customers.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("Domain.Customers.Entities.Orders.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("ShopOrders");
                });

            modelBuilder.Entity("Domain.Customers.Entities.ShoppingCarts.ShoppingCart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Domain.Shops.Entities.ShopOrders.ShopOrder", b =>
                {
                    b.Navigation("ShopOrderItems");
                });

            modelBuilder.Entity("Domain.Shops.Shop", b =>
                {
                    b.Navigation("ProductList");

                    b.Navigation("ShopOrdersList");
                });
#pragma warning restore 612, 618
        }
    }
}
