﻿namespace RSMEnterpriseIntegrationsAPI.Application.Validators.SalesOrderHeader
{
    using FluentValidation;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.SalesOrderHeader;

    public class CreateSalesOrderHeaderDtoValidator : AbstractValidator<CreateSalesOrderHeaderDto>
    {
        public CreateSalesOrderHeaderDtoValidator()
        {
            RuleFor(s => s.OrderDate)
                .NotEmpty();

            RuleFor(s => s.DueDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(s => s.OrderDate);

            RuleFor(s => s.ShipDate)
                .GreaterThanOrEqualTo(s => s.OrderDate)
                .When(s => s.ShipDate is not null);

            RuleFor(s => s.SubTotal)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(s => s.TaxAmt)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(s => s.Freight)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(s => s.Comment)
                .MaximumLength(128)
                .When(s => s.Comment is not null);

        }
    }
}
