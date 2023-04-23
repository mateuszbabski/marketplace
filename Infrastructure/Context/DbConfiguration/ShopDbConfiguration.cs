using Domain.Customers.ValueObjects;
using Domain.Shops;
using Domain.Shops.ValueObjects;
using Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.ValueObjects;
using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Entities.Orders;
using Domain.Shops.Entities.ShopOrders;
using Domain.Shops.Entities.ShopOrders.ValueObjects;

namespace Infrastructure.Context.DbConfiguration
{
    internal sealed class ShopDbConfiguration : IEntityTypeConfiguration<Shop>, IEntityTypeConfiguration<Product>, IEntityTypeConfiguration<ShopOrder>, IEntityTypeConfiguration<ShopOrderItem>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            var passwordConverter = new ValueConverter<PasswordHash, string>(c => c.Value, c => new PasswordHash(c));

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new ShopId(c));

            builder.Property(c => c.Email)
                   .HasConversion(c => c.Value, c => new Email(c));

            builder.Property(c => c.PasswordHash)
                   .HasConversion(passwordConverter);

            builder.Property(c => c.OwnerName)
                   .HasConversion(c => c.Value, c => new Name(c));

            builder.Property(c => c.OwnerLastName)
                   .HasConversion(c => c.Value, c => new LastName(c));

            builder.Property(c => c.ShopName)
                   .HasConversion(c => c.Value, c => new ShopName(c));

            builder.Property(c => c.TaxNumber)
                   .HasConversion(c => c.Value, c => new TaxNumber(c));

            builder.OwnsOne(c => c.ShopAddress, sa =>
            {
                sa.Property(x => x.Country).HasColumnName("Country");
                sa.Property(x => x.City).HasColumnName("City");
                sa.Property(x => x.Street).HasColumnName("Street");
                sa.Property(x => x.PostalCode).HasColumnName("PostalCode");
            });

            builder.Property(c => c.ContactNumber)
                   .HasConversion(c => c.Value, c => new TelephoneNumber(c));

            builder.Property(c => c.Role)
                   .HasConversion(v => v.ToString(),
                                   v => (Roles)Enum.Parse(typeof(Roles), v));

            builder.HasMany<Product>(c => c.ProductList)
                   .WithOne(p => p.Shop)
                   .HasForeignKey(p => p.ShopId);

            builder.HasMany<ShopOrder>(c => c.ShopOrdersList)
                   .WithOne(p => p.Shop)
                   .HasForeignKey(p => p.ShopId);

            builder.ToTable("Shops");
        }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new ProductId(c));

            builder.Property(c => c.ProductName)
                   .HasConversion(c => c.Value, c => new ProductName(c));

            builder.Property(c => c.ProductDescription)
                   .HasConversion(c => c.Value, c => new ProductDescription(c));

            builder.Property(c => c.Unit)
                   .HasConversion(c => c.Value, c => new ProductUnit(c));

            builder.HasOne<Shop>(c => c.Shop)
                   .WithMany(p => p.ProductList)
                   .HasForeignKey(c => c.ShopId);

            builder.Property(c => c.IsAvailable);

            builder.OwnsOne(c => c.Price, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount");
            });

            builder.ToTable("Products");
        }

        public void Configure(EntityTypeBuilder<ShopOrder> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new ShopOrderId(c));

            builder.Property(c => c.CustomerId)
                   .HasConversion(c => c.Value, c => new CustomerId(c));

            builder.OwnsOne(c => c.ShippingAddress, sa =>
            {
                sa.Property(x => x.Country).HasColumnName("Country");
                sa.Property(x => x.City).HasColumnName("City");
                sa.Property(x => x.Street).HasColumnName("Street");
                sa.Property(x => x.PostalCode).HasColumnName("PostalCode");
            });

            builder.OwnsOne(c => c.TotalPrice, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount");
            });

            builder.Property(c => c.ShopOrderStatus)
                   .HasConversion(v => v.ToString(),
                                  v => (ShopOrderStatus)Enum.Parse(typeof(ShopOrderStatus), v));

            builder.Property(c => c.PlacedOn).HasColumnName("PlacedOn");

            builder.Property(c => c.StatusChanged).HasColumnName("StatusChanged");

            builder.HasOne(c => c.Shop)
                   .WithMany(p => p.ShopOrdersList)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.ShopOrderItems)
                   .WithOne(c => c.ShopOrder)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("ShopOrders");
        }

        public void Configure(EntityTypeBuilder<ShopOrderItem> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new ShopOrderItemId(c));

            builder.Property(c => c.ProductId)
                   .HasConversion(c => c.Value, c => new ProductId(c));

            builder.HasOne(c => c.ShopOrder)
                   .WithMany(p => p.ShopOrderItems)
                   .HasForeignKey(c => c.ShopOrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Quantity)
                   .HasColumnName("Quantity");

            builder.OwnsOne(c => c.Price, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount");
            });

            builder.ToTable("ShopOrderItems");
        }
    }
}
