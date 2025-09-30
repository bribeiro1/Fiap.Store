using MediatR;

namespace Fiap.Store.Application.Commands.Update;

public sealed record ApplyBestDiscountCommand(string OrderId, string? Coupon) : IRequest<decimal>;