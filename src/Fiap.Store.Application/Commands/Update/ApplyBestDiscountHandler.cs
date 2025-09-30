using Fiap.Store.Domain.Contracts;
using Fiap.Store.Domain.ValueObjects;
using MediatR;

namespace Fiap.Store.Application.Commands.Update;

public sealed class ApplyBestDiscountHandler(IOrderRepository repository, IOrderService service) : IRequestHandler<ApplyBestDiscountCommand, decimal>
{
    public async Task<decimal> Handle(ApplyBestDiscountCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.OrderId, out var guid))
            throw new ArgumentException("Invalid order id", nameof(request.OrderId));

        var order = await repository.GetAsync(new OrderId(guid), cancellationToken)
                   ?? throw new KeyNotFoundException("Order not found");

        return service.ApplyBestDiscount(order, request.Coupon);
    }
}