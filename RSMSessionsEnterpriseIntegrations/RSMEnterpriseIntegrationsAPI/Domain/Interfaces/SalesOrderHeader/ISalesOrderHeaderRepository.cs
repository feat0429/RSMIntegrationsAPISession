
namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces.SalesOrderHeader
{
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public interface ISalesOrderHeaderRepository
    {
        Task<SalesOrderHeader?> GetSalesOrderHeaderById(int id);
        Task<PagedList<SalesOrderHeader>> GetPaginatedSalesOrderHeaders(int page, int pageSize);
        Task<int> CreateSalesOrderHeader(SalesOrderHeader salesOrderHeader);
        Task<int> UpdateSalesOrderHeader(SalesOrderHeader salesOrderHeader);
        Task<int> DeleteSalesOrderHeader(SalesOrderHeader salesOrderHeader);
    }
}
