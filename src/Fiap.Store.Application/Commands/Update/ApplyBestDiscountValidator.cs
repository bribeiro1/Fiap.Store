using FluentValidation;

namespace Fiap.Store.Application.Commands.Update;

public sealed class ApplyBestDiscountValidator : AbstractValidator<ApplyBestDiscountCommand>
{
    public ApplyBestDiscountValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
    }
}