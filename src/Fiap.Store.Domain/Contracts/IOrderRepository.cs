using Fiap.Store.Domain.Entities;
using Fiap.Store.Domain.ValueObjects;

namespace Fiap.Store.Domain.Contracts;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order?> GetAsync(OrderId id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
}