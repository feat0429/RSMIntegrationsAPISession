namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Department;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AdvWorksDbContext _context;
        public DepartmentRepository(AdvWorksDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateDepartment(Department department)
        {
            await _context.AddAsync(department);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteDepartment(Department department)
        {
            _context.Remove(department);

            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _context.Set<Department>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Department?> GetDepartmentById(int id)
        {
            return await _context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d=>d.DepartmentId == id);
        }

        public async Task<int> UpdateDepartment(Department department)
        {
            _context.Update(department);

            return await _context.SaveChangesAsync();
        }
    }
}
