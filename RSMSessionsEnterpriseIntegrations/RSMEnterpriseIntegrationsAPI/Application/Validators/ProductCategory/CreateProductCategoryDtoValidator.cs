using FluentValidation;
using RSMEnterpriseIntegrationsAPI.Application.DTOs.ProductCategory;

namespace RSMEnterpriseIntegrationsAPI.Application.Validators.ProductCategory
{
    public class CreateProductCategoryDtoValidator : AbstractValidator<CreateProductCategoryDto>
    {
        public CreateProductCategoryDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
