namespace RSMEnterpriseIntegrationsAPI.Application.DTOs.SalesOrderHeader
{
    public record CreateSalesOrderHeaderDto
    (
        DateTime OrderDate,
        DateTime DueDate,
        DateTime? ShipDate,
        bool? OnlineOrderFlag,
        decimal SubTotal,
        decimal TaxAmt,
        decimal Freight,
        string? Comment
    );
}
