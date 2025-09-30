using Fiap.Store.Application.Commands.Update;
using Fiap.Store.Domain.Contracts;
using Fiap.Store.Domain.Entities;
using Fiap.Store.Domain.Services;
using Fiap.Store.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.Store.Tests.Handlers;

public class ApplyBestDiscountHandlerTests
{
    [Fact]
    public async Task Deve_retornar_preco_final()
    {
        var order = new Order();
        order.AddItem(new OrderItem("A", 2, 300));

        var repo = new Mock<IOrderRepository>();
        repo.Setup(r => r.GetAsync(It.IsAny<OrderId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        var service = new Mock<IOrderService>();
        service.Setup(p => p.ApplyBestDiscount(order, "OFF20")).Returns(480m);

        var handler = new ApplyBestDiscountHandler(repo.Object, service.Object);
        var result = await handler.Handle(new ApplyBestDiscountCommand(Guid.NewGuid().ToString(), "OFF20"), default);

        result.Should().Be(480m);
    }

    [Fact]
    public async Task Deve_lancar_quando_nao_encontrar_order()
    {
        var repo = new Mock<IOrderRepository>();
        repo.Setup(r => r.GetAsync(It.IsAny<OrderId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order?)null);

        var handler = new ApplyBestDiscountHandler(repo.Object, new OrderService());

        Func<Task> act = async () => await handler.Handle(new ApplyBestDiscountCommand(Guid.NewGuid().ToString(), null), default);
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}