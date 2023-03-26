using Domain.Customers.ValueObjects;
using Domain.Shop;
using Domain.Shop.ValueObjects;
using Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Shop.Entities.Products;
using Domain.Shop.Entities.Products.ValueObjects;

namespace Infrastructure.Context.DbConfiguration
{
    internal sealed class ShopDbConfiguration : IEntityTypeConfiguration<Shop>, IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            var passwordConverter = new ValueConverter<PasswordHash, string>(c => c.Value, c => new PasswordHash(c));
            //var addressConverter = new ValueConverter<Address, string>(c => c.ToString(), c => Address.CreateAddress(c));

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

            //builder.Property(c => c.ShopAddress)
            //       .HasConversion(addressConverter);

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

            builder.HasMany<Product>(c => c.Products)
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
                   .WithMany(p => p.Products)
                   .HasForeignKey(c => c.ShopId);

            builder.Property(c => c.IsAvailable);

            builder.OwnsOne(c => c.Price, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount");
            });

            builder.ToTable("Products");
        }
    }
}
