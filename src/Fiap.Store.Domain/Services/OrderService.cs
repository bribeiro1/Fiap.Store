using Fiap.Store.Domain.Contracts;
using Fiap.Store.Domain.Entities;

namespace Fiap.Store.Domain.Services;

public sealed class OrderService : IOrderService
{
    public decimal ApplyBestDiscount(Order order, string? coupon)
    {
        if (order is null) throw new ArgumentNullException(nameof(order));

        decimal bySubtotal = 0m;
        var subtotal = order.Subtotal;

        if (subtotal >= 1000m) bySubtotal = 0.10m;
        else if (subtotal >= 500m) bySubtotal = 0.05m;

        decimal byCoupon = 0m;
        if (!string.IsNullOrWhiteSpace(coupon))
        {
            if (coupon.Equals("OFF20", StringComparison.OrdinalIgnoreCase))
                byCoupon = 0.20m;
            else
                throw new ArgumentException("Invalid coupon", nameof(coupon));
        }

        var best = Math.Max(bySubtotal, byCoupon);
        return Math.Round(subtotal * (1 - best), 2);
    }
}