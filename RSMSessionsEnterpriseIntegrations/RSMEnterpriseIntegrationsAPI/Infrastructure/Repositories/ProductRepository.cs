namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Product;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public class ProductRepository : IProductRepository
    {
        private readonly AdvWorksDbContext _context;

        public ProductRepository(AdvWorksDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.ProductId;
        }

        public async Task<int> DeleteProduct(Product product)
        {
            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<int> UpdateProduct(Product product)
        {
            _context.Update(product);

            return await _context.SaveChangesAsync();
        }
    }
}
