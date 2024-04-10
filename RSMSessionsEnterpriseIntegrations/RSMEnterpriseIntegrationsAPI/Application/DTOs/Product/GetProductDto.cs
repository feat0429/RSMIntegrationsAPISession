namespace RSMEnterpriseIntegrationsAPI.Application.DTOs.Product
{
    public record GetProductDto
    (
        int ProductId,
        string? Name,
        string? ProductNumber,
        short SafetyStockLevel,
        short ReorderPoint,
        decimal StandardCost,
        decimal ListPrice,
        int DaysToManufacture,
        DateTime SellStartDate
    );
}
