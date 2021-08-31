using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Category;

namespace Product.Infrastructure.Persistence.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryAggregate>
    {
        public void Configure(EntityTypeBuilder<CategoryAggregate> builder)
        {
            builder.ToTable("categories");

            builder.Property(p => p.Title).HasColumnName("title");
            builder.Property(p => p.Status).HasColumnName("status");
            builder.Property(p => p.ParentId).HasColumnName("pid");
            builder.Property(p => p.MaxStockQuantity).HasColumnName("max_stock_quantity");
            builder.Property(p => p.MinStockQuantity).HasColumnName("min_stock_quantity");

            builder.HasIndex(p => p.ParentId);
        }
    }
}