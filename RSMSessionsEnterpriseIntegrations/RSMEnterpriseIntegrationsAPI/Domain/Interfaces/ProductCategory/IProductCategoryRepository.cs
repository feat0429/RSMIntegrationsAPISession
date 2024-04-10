namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces.ProductCategory
{
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public interface IProductCategoryRepository
    {
        Task<ProductCategory?> GetProductCategoryById(int id);
        Task<IEnumerable<ProductCategory>> GetAllProductCategories();
        Task<int> CreateProductCategory(ProductCategory productCategory);
        Task<int> UpdateProductCategory(ProductCategory productCategory);
        Task<int> DeleteProductCategory(ProductCategory productCategory);
    }
}
