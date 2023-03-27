using Domain.Customers;
using Domain.Customers.ValueObjects;
using Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Context.DbConfiguration
{
    internal sealed class CustomerDbConfiguration : IEntityTypeConfiguration<Customer>
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

            builder.Property(c => c.Role)
                   .HasConversion(v => v.ToString(),
                                   v => (Roles)Enum.Parse(typeof(Roles), v));

            builder.ToTable("Customers");
        }
    }
}
