namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Product
{
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public interface IProductRepository
    {
        Task<Product?> GetProductById(int id);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<int> CreateProduct(Product product);
        Task<int> UpdateProduct(Product product);
        Task<int> DeleteProduct(Product product);
    }
}
