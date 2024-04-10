
namespace RSMEnterpriseIntegrationsAPI.Application.Validators.Product
{
    using FluentValidation;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.Product;

    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(p => p.ProductId)
               .NotNull()
               .GreaterThan(0);

            RuleFor(p => p.Name)
               .NotEmpty()
               .MaximumLength(50);

            RuleFor(p => p.ProductNumber)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.SafetyStockLevel)
                .NotNull()
                .GreaterThan((short)0);

            RuleFor(p => p.ReorderPoint)
                .NotNull()
                .GreaterThan((short)0);

            RuleFor(p => p.StandardCost)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.ListPrice)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.DaysToManufacture)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.SellStartDate)
                .NotEmpty();
        }
    }
}
