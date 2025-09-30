using Fiap.Store.Domain.Entities;

namespace Fiap.Store.Domain.Contracts;

public interface IOrderService
{
    decimal ApplyBestDiscount(Order order, string? coupon);
}