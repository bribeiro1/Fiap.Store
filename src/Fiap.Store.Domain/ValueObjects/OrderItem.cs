namespace Fiap.Store.Domain.ValueObjects;

public sealed record OrderItem(string Sku, int Quantity, decimal UnitPrice)
{
    public decimal LineTotal => Quantity * UnitPrice;
}