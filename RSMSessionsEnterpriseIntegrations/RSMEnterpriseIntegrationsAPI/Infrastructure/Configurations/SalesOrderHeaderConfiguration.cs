namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public class SalesOrderHeaderConfiguration : IEntityTypeConfiguration<SalesOrderHeader>
    {
        public void Configure(EntityTypeBuilder<SalesOrderHeader> builder)
        {
            builder.ToTable(nameof(SalesOrderHeader), "Sales", tb =>
            {
                tb.HasTrigger("uSalesOrderHeader");
            });

            builder.HasKey(e => e.SalesOrderId);

            builder.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");
            builder.Property(e => e.BillToAddressId).HasColumnName("BillToAddressID");
            builder.Property(e => e.CreditCardId).HasColumnName("CreditCardID");
            builder.Property(e => e.CurrencyRateId).HasColumnName("CurrencyRateID");
            builder.Property(e => e.CustomerId).HasColumnName("CustomerID");
            builder.Property(e => e.SalesPersonId).HasColumnName("SalesPersonID");
            builder.Property(e => e.ShipMethodId).HasColumnName("ShipMethodID");
            builder.Property(e => e.ShipToAddressId).HasColumnName("ShipToAddressID");
            builder.Property(e => e.TerritoryId).HasColumnName("TerritoryID");
        }
    }
}
