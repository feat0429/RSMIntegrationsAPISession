namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Product
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.Product;

    public interface IProductService
    {
        Task<GetProductDto?> GetProductById(int id);
        Task<IEnumerable<GetProductDto>> GetAllProducts();
        Task<int> CreateProduct(CreateProudctDto productDto);
        Task<int> UpdateProduct(UpdateProductDto productDto);
        Task<int> DeleteProduct(int id);
    }
}
