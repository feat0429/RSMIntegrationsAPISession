namespace RSMEnterpriseIntegrationsAPI.Application.DTOs.Department
{
    public class UpdateDepartmentDto
    {
        public short DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
    }
}
