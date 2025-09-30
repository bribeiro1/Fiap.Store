using Fiap.Store.Domain.Contracts;
using Fiap.Store.Domain.Entities;
using Fiap.Store.Domain.ValueObjects;
using System.Collections.Concurrent;

namespace Fiap.Store.Infra.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private static readonly ConcurrentDictionary<OrderId, Order> _db = new();

    public Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        _db[order.Id] = order;
        return Task.CompletedTask;
    }

    public Task<Order?> GetAsync(OrderId id, CancellationToken cancellationToken = default)
    {
        _db.TryGetValue(id, out var order);
        return Task.FromResult(order);
    }

    public Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        _db[order.Id] = order;
        return Task.CompletedTask;
    }
}