using FluentValidation;

namespace Fiap.Store.Application.Commands.Create;

public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.Items).NotEmpty();
        RuleForEach(x => x.Items).ChildRules(i =>
        {
            i.RuleFor(x => x.Sku).NotEmpty();
            i.RuleFor(x => x.Quantity).GreaterThan(0);
            i.RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
        });
    }
}