namespace Fiap.Store.Application.Dtos;

public sealed record OrderDto(string Id, decimal Subtotal, IReadOnlyCollection<OrderItemDto> Items);