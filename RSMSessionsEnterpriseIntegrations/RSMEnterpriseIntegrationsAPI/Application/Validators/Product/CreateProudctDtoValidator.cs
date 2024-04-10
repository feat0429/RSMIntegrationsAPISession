namespace RSMEnterpriseIntegrationsAPI.Application.Validators.Product
{
    using FluentValidation;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.Product;

    public class CreateProudctDtoValidator : AbstractValidator<CreateProudctDto>
    {
        public CreateProudctDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.ProductNumber)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.SafetyStockLevel)
                .GreaterThan((short)0);

            RuleFor(p => p.ReorderPoint)
                .GreaterThan((short)0);

            RuleFor(p => p.StandardCost)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.ListPrice)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.DaysToManufacture)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.SellStartDate)
                .NotEmpty();
        }
    }
}
