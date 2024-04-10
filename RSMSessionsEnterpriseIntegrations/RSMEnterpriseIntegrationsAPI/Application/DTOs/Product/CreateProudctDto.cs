namespace RSMEnterpriseIntegrationsAPI.Application.DTOs.Product
{
    public record CreateProudctDto
    (
        string Name,
        string ProductNumber,
        short SafetyStockLevel,
        short ReorderPoint,
        decimal StandardCost,
        decimal ListPrice,
        int DaysToManufacture,
        DateTime SellStartDate
    );
}
