namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.SalesOrderHeader;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;
    using System.Threading.Tasks;

    public class SalesOrderHeaderRepository : ISalesOrderHeaderRepository
    {
        private readonly AdvWorksDbContext _context;

        public SalesOrderHeaderRepository(AdvWorksDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            _context.Add(salesOrderHeader);
            await _context.SaveChangesAsync();

            return salesOrderHeader.SalesOrderId;
        }

        public async Task<int> DeleteSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            _context.Remove(salesOrderHeader);

            return await _context.SaveChangesAsync();
        }

        //Added pagination to SalesOrderHeader model because a single query to get all the records took long.
        public async Task<PagedList<SalesOrderHeader>> GetPaginatedSalesOrderHeaders(int page, int pageSize)
        {
            var query = _context.SalesOrderHeaders
                 .AsNoTracking();

            return await PagedList<SalesOrderHeader>
                .CreateAsync(query, page, pageSize);
        }

        public async Task<SalesOrderHeader?> GetSalesOrderHeaderById(int id)
        {

            return await _context.SalesOrderHeaders
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SalesOrderId == id);
        }

        public async Task<int> UpdateSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            _context.Update(salesOrderHeader);

            return await _context.SaveChangesAsync();
        }
    }
}
