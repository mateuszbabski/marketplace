using Domain.Customers;
using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.Entities.ShoppingCarts.ValueObjects;
using Domain.Customers.ValueObjects;
using Domain.Shared.ValueObjects;
using Domain.Shops;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.ValueObjects;
using Domain.Shops.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Context.DbConfiguration
{
    internal sealed class CustomerDbConfiguration : IEntityTypeConfiguration<Customer>, IEntityTypeConfiguration<ShoppingCart>, IEntityTypeConfiguration<ShoppingCartItem>, IEntityTypeConfiguration<Order>, IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            var passwordConverter = new ValueConverter<PasswordHash, string>(c => c.Value, c => new PasswordHash(c));

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new CustomerId(c));

            builder.Property(c => c.Email)
                   .HasConversion(c => c.Value, c => new Email(c));

            builder.Property(c => c.PasswordHash)
                   .HasConversion(passwordConverter);

            builder.Property(c => c.Name)
                   .HasConversion(c => c.Value, c => new Name(c));

            builder.Property(c => c.LastName)
                   .HasConversion(c => c.Value, c => new LastName(c));            

            builder.Property(c => c.TelephoneNumber)
                   .HasConversion(c => c.Value, c => new TelephoneNumber(c));

            builder.OwnsOne(c => c.Address, sa =>
            {
                sa.Property(x => x.Country).HasColumnName("Country");
                sa.Property(x => x.City).HasColumnName("City");
                sa.Property(x => x.Street).HasColumnName("Street");
                sa.Property(x => x.PostalCode).HasColumnName("PostalCode");
            });

            builder.HasMany(c => c.Orders)
                   .WithOne(i => i.Customer)
                   .HasForeignKey(c => c.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Role)
                   .HasColumnName("Role");           
                
            builder.ToTable("Customers");
        }

        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new ShoppingCartId(c));

            builder.Property(c => c.CustomerId)
                   .HasConversion(c => c.Value, c => new CustomerId(c));

            builder.OwnsOne(c => c.TotalPrice, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount");
            });

            builder.HasMany(c => c.Items)
                   .WithOne(i => i.ShoppingCart)
                   .HasForeignKey(c => c.ShoppingCartId)
                   .OnDelete(DeleteBehavior.Cascade);            

            builder.ToTable("ShoppingCarts");
        }

        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new ShoppingCartItemId(c));

            builder.HasOne(c => c.ShoppingCart)
                   .WithMany(p => p.Items)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(c => c.Price, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount");
            });

            builder.Property(c => c.ProductId)
                   .HasConversion(c => c.Value, c => new ProductId(c));

            builder.Property(c => c.ShopId)
                   .HasConversion(c => c.Value, c => new ShopId(c));

            builder.Property(c => c.Quantity).HasColumnName("Quantity");

            builder.ToTable("ShoppingCartItems");
        }

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new OrderId(c));

            builder.OwnsOne(c => c.TotalPrice, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount");
            });

            builder.Property(c => c.PlacedOn).HasColumnName("PlacedOn");

            builder.Property(c => c.StatusChanged).HasColumnName("StatusChanged");

            builder.Property(c => c.OrderStatus)
                   .HasColumnName("OrderStatus");

            builder.OwnsOne(c => c.ShippingAddress, sa =>
            {
                sa.Property(x => x.Country).HasColumnName("Country");
                sa.Property(x => x.City).HasColumnName("City");
                sa.Property(x => x.Street).HasColumnName("Street");
                sa.Property(x => x.PostalCode).HasColumnName("PostalCode");
            });

            builder.HasOne(c => c.Customer)
                   .WithMany(p => p.Orders)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.OrderItems)
                   .WithOne(c => c.Order)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.ShopOrders)
                   .WithOne(c => c.Order)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Orders");
        }

        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new OrderItemId(c));

            builder.Property(c => c.ProductId)
                   .HasConversion(c => c.Value, c => new ProductId(c));

            builder.HasOne(c => c.Order)
                   .WithMany(p => p.OrderItems)
                   .HasForeignKey(c => c.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Quantity)
                   .HasColumnName("Quantity");

            builder.OwnsOne(c => c.Price, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount");
            });

            builder.ToTable("OrderItems");
        }
    }
}
