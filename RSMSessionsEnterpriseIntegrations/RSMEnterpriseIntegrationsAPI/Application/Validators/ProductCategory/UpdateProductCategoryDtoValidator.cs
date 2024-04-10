namespace RSMEnterpriseIntegrationsAPI.Application.Validators.ProductCategory
{
    using FluentValidation;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.ProductCategory;

    public class UpdateProductCategoryDtoValidator : AbstractValidator<UpdateProductCategoryDto>
    {
        public UpdateProductCategoryDtoValidator()
        {
            RuleFor(c => c.ProductCategoryId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(c => c.Name)
                .NotEmpty();
        }
    }
}
