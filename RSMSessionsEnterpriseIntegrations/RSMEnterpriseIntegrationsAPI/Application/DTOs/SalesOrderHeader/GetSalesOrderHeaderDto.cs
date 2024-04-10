namespace RSMEnterpriseIntegrationsAPI.Application.DTOs.SalesOrderHeader
{
    public record GetSalesOrderHeaderDto
    (
        int SalesOrderId,
        byte RevisionNumber,
        DateTime OrderDate,
        DateTime DueDate,
        DateTime? ShipDate,
        byte Status,
        bool OnlineOrderFlag,
        string? SalesOrderNumber,
        string? AccountNumber,
        int CustomerId,
        int? SalesPersonId,
        int? TerritoryId,
        int BillToAddressId,
        int ShipToAddressId,
        int ShipMethodId,
        decimal SubTotal,
        decimal TaxAmt,
        decimal Freight,
        decimal TotalDue,
        string Comment
    );
}
