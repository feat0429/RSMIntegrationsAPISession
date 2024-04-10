namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.ProductCategory;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly AdvWorksDbContext _context;

        public ProductCategoryRepository(AdvWorksDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateProductCategory(ProductCategory productCategory)
        {
            _context.Add(productCategory);
            await _context.SaveChangesAsync();

            return productCategory.ProductCategoryId;
        }

        public async Task<int> DeleteProductCategory(ProductCategory productCategory)
        {
            _context.Remove(productCategory);

            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetAllProductCategories()
        {
            return await _context.ProductCategories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductCategory?> GetProductCategoryById(int id)
        {
            return await _context.ProductCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ProductCategoryId == id);
        }

        public async Task<int> UpdateProductCategory(ProductCategory productCategory)
        {
            _context.Update(productCategory);

            return await _context.SaveChangesAsync();
        }
    }
}
