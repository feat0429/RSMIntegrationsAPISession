namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Department
{
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public interface IDepartmentRepository
    {
        Task<Department?> GetDepartmentById(int id);
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<int> CreateDepartment(Department department);
        Task<int> UpdateDepartment(Department department);
        Task<int> DeleteDepartment(Department department);
    }
}
