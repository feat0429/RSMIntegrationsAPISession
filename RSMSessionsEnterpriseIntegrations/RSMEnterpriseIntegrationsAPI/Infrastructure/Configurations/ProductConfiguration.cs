namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.ProductId).HasName("PK_Product_ProductID");

            builder.ToTable(nameof(Product), "Production");

            builder.Property(e => e.ProductId).HasColumnName("ProductID");
            builder.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            builder.Property(e => e.ProductSubcategoryId).HasColumnName("ProductSubcategoryID");
        }
    }
}
