namespace RSMEnterpriseIntegrationsAPI.Application.DTOs.Department
{
    public record GetDepartmentDto
    (
        short DepartmentId,
        string? Name,
        string? GroupName
    );
}
