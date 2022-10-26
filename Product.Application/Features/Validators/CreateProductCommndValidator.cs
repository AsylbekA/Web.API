using FluentValidation;
using Product.Application.Features.Commands;

namespace Product.Application.Features.Validators;

public class CreateProductCommndValidator:AbstractValidator<CreateProductCommand>
{
    public CreateProductCommndValidator()
    {
        RuleFor(c => c.Barcode).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Price).NotEqual(0).ScalePrecision(2, 8);
    }
}
