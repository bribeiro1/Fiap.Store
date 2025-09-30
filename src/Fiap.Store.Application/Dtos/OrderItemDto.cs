namespace Fiap.Store.Application.Dtos;

public sealed record OrderItemDto(string Sku, int Quantity, decimal UnitPrice);