using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.ValueObjects;
using Domain.Invoices;
using Domain.Invoices.ValueObjects;
using Domain.Shops.Entities.ShopOrders.ValueObjects;
using Domain.Shops.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.DbConfiguration
{
    internal sealed class InvoiceDbConfiguration : IEntityTypeConfiguration<Invoice>, IEntityTypeConfiguration<ShopInvoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new InvoiceId(c));

            builder.Property(c => c.CustomerId)
                   .HasConversion(c => c.Value, c => new CustomerId(c));

            builder.Property(c => c.OrderId)
                   .HasConversion(c => c.Value, c => new OrderId(c));

            builder.Property(c => c.CreatedOn).HasColumnName("CreatedOn");

            builder.Property(c => c.InvoiceStatus).HasColumnName("InvoiceStatus");

            builder.Property(c => c.DateOfPayment).HasColumnName("DateOfPayment");

            builder.OwnsOne(c => c.TotalPrice, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount").HasPrecision(18, 2);
            });

            builder.HasMany(c => c.ShopInvoices)
                   .WithOne(c => c.Invoice)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Invoices");
        }

        public void Configure(EntityTypeBuilder<ShopInvoice> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new ShopInvoiceId(c));

            builder.Property(c => c.ShopId)
                   .HasConversion(c => c.Value, c => new ShopId(c));

            builder.Property(c => c.ShopOrderId)
                   .HasConversion(c => c.Value, c => new ShopOrderId(c));

            builder.Property(c => c.CreatedOn).HasColumnName("CreatedOn");

            builder.Property(c => c.InvoiceStatus).HasColumnName("InvoiceStatus");

            builder.Property(c => c.DateOfPayment).HasColumnName("DateOfPayment");

            builder.OwnsOne(c => c.PartialOrderPrice, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount").HasPrecision(18, 2);
            });

            builder.HasOne(c => c.Invoice)
                   .WithMany(p => p.ShopInvoices)
                   .HasForeignKey(c => c.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("ShopInvoices");
        }
    }
}
