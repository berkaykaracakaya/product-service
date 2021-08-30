using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Product;

namespace Product.Infrastructure.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductAggregate>
    {
        public void Configure(EntityTypeBuilder<ProductAggregate> builder)
        {
            builder.ToTable("products");

            builder.Property(p => p.Title).HasColumnName("title");
            builder.Property(p => p.Quantity).HasColumnName("quantity");
            builder.Property(p => p.Description).HasColumnName("description");
            builder.Property(p => p.Categories).HasColumnName("categories").HasColumnType("jsonb");
        }
    }
}