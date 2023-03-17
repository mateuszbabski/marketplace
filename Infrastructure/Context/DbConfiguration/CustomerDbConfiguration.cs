using Domain.Customers;
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
            var addressConverter = new ValueConverter<Address, string>(c => c.ToString(), c => Address.CreateAddress(c));

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

            builder.Property(c => c.Role)
                   .HasConversion(v => v.ToString(),
                                   v => (Roles)Enum.Parse(typeof(Roles), v));

            builder.Property(c => c.Address)
                   .HasConversion(addressConverter);                  

            builder.ToTable("Customers");
        }
    }
}
