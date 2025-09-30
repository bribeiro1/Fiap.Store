using Fiap.Store.Domain.ValueObjects;

namespace Fiap.Store.Domain.Entities;

public sealed class Order
{
    private readonly List<OrderItem> _items = new();

    public OrderId Id { get; }
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public decimal Subtotal => Math.Round(_items.Sum(i => i.LineTotal), 2);

    public Order(OrderId? id = null)
    {
        Id = id ?? OrderId.New();
    }

    public void AddItem(OrderItem item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        if (item.Quantity <= 0) throw new ArgumentOutOfRangeException(nameof(item.Quantity));
        if (item.UnitPrice < 0) throw new ArgumentOutOfRangeException(nameof(item.UnitPrice));
        _items.Add(item);
    }
}