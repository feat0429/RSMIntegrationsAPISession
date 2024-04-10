namespace RSMEnterpriseIntegrationsAPI.Application.DTOs.SalesOrderHeader
{
    public record UpdateSalesOrderHeaderDto
    (
        int SalesOrderId,
        DateTime OrderDate,
        DateTime DueDate,
        DateTime? ShipDate,
        byte Status,
        decimal SubTotal,
        decimal TaxAmt,
        decimal Freight,
        string Comment
    );
}
