using Domain.Shared.ValueObjects;
using Domain.Shop.Entities.Products;
using Domain.Shop.Entities.Products.ValueObjects;
using Domain.Shop.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.DbConfiguration
{
    internal sealed class ProductDbConfiguration : IEntityTypeConfiguration<Product>
    {
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

            builder.Property(c => c.ShopId)
                   .HasConversion(c => c.Value, c => new ShopId(c));

            builder.Property(c => c.IsAvailable);

            builder.OwnsOne<MoneyValue>("Value", mv =>
            {
                mv.Property(p => p.Currency).HasColumnName("Currency");
                mv.Property(p => p.Value).HasColumnName("Value");
            });

            builder.ToTable("Products");
        }
    }
}
