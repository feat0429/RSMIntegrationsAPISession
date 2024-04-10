
namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces.SalesOrderHeader
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.PagedList;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.SalesOrderHeader;

    public interface ISalesOrderHeaderService
    {
        Task<GetSalesOrderHeaderDto?> GetSalesOrderHeaderById(int id);
        Task<GetPagedListDto<GetSalesOrderHeaderDto>> GetPaginatedSalesOrderHeaders(int page, int pageSize);
        Task<int> CreateSalesOrderHeader(CreateSalesOrderHeaderDto salesOrderHeaderDto);
        Task<int> UpdateSalesOrderHeader(UpdateSalesOrderHeaderDto salesOrderHeaderDto);
        Task<int> DeleteSalesOrderHeader(int id);
    }
}
