namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces.ProductCategory
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.ProductCategory;

    public interface IProductCategoryService
    {
        Task<GetProductCategoryDto?> GetProductCategoryById(int id);
        Task<IEnumerable<GetProductCategoryDto>> GetAllProductCategories();
        Task<int> CreateProductCategory(CreateProductCategoryDto productDto);
        Task<int> UpdateProductCategory(UpdateProductCategoryDto productDto);
        Task<int> DeleteProductCategory(int id);
    }
}
