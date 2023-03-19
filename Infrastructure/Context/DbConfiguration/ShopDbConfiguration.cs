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

namespace Infrastructure.Context.DbConfiguration
{
    internal sealed class ShopDbConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            var passwordConverter = new ValueConverter<PasswordHash, string>(c => c.Value, c => new PasswordHash(c));
            var addressConverter = new ValueConverter<Address, string>(c => c.ToString(), c => Address.CreateAddress(c));

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

            builder.Property(c => c.ShopAddress)
                   .HasConversion(addressConverter);

            builder.Property(c => c.ContactNumber)
                   .HasConversion(c => c.Value, c => new TelephoneNumber(c));

            builder.Property(c => c.Role)
                   .HasConversion(v => v.ToString(),
                                   v => (Roles)Enum.Parse(typeof(Roles), v));            

            builder.ToTable("Shops");
        }
    }
}
