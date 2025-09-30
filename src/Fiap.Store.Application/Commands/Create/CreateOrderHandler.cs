using Fiap.Store.Application.Dtos;
using Fiap.Store.Domain.Contracts;
using Fiap.Store.Domain.Entities;
using Fiap.Store.Domain.ValueObjects;
using MediatR;

namespace Fiap.Store.Application.Commands.Create;

public sealed class CreateOrderHandler(IOrderRepository repository) : IRequestHandler<CreateOrderCommand, OrderDto>
{
    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order();
        foreach (var it in request.Items)
            order.AddItem(new OrderItem(it.Sku, it.Quantity, it.UnitPrice));

        await repository.AddAsync(order, cancellationToken);

        var dto = new OrderDto(order.Id.ToString(), order.Subtotal,
            order.Items.Select(i => new OrderItemDto(i.Sku, i.Quantity, i.UnitPrice)).ToList());

        return dto;
    }
}