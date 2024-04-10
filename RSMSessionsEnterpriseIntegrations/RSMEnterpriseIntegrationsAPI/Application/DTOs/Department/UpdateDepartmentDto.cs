namespace RSMEnterpriseIntegrationsAPI.Application.DTOs.Department
{
    public record UpdateDepartmentDto
    (
        short DepartmentId,
        string Name,
        string GroupName
    );
}
