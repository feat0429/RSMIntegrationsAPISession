namespace RSMEnterpriseIntegrationsAPI.Application.DTOs.PagedList
{
    public record GetPagedListDto<T>
    (
        int PageSize,
        int CurrentPage,
        int TotalItemCount,
        int TotalPageCount,
        bool HasPrevious,
        bool HasNext,
        List<T> Items
    );
}
