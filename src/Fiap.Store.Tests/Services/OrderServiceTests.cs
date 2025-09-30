using Fiap.Store.Domain.Entities;
using Fiap.Store.Domain.Services;
using Fiap.Store.Domain.ValueObjects;
using FluentAssertions;
using System;
using Xunit;

namespace Fiap.Store.Tests.Services;

public class OrderServiceTests
{
    [Theory]
    [InlineData(400, 0, null, 400)]
    [InlineData(500, 0, null, 475)]
    [InlineData(1000, 0, null, 900)]
    [InlineData(600, 0, "OFF20", 480)]
    [InlineData(1200, 0, "OFF20", 960)]
    public void ApplyBest_calcula(decimal subtotal, int dummy, string? coupon, decimal expected)
    {
        var order = new Order();
        order.AddItem(new OrderItem("SKU", 1, subtotal));

        var service = new OrderService();
        var final = service.ApplyBestDiscount(order, coupon);

        final.Should().Be(expected);
    }

    [Fact]
    public void ApplyBest_lanca_para_cupom_invalido()
    {
        var order = new Order();
        order.AddItem(new OrderItem("SKU", 1, 500m));

        var service = new OrderService();
        Action act = () => service.ApplyBestDiscount(order, "BAD");
        act.Should().Throw<ArgumentException>()
           .WithParameterName("coupon")
           .WithMessage("*Invalid coupon*");
    }
}