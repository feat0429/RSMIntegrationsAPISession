using FluentValidation;
using RSMEnterpriseIntegrationsAPI.Application.DTOs.Department;

namespace RSMEnterpriseIntegrationsAPI.Application.Validators.Department
{
    public class UpdateDepartmentDtoValidator : AbstractValidator<UpdateDepartmentDto>
    {
        public UpdateDepartmentDtoValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(d => d.GroupName)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
