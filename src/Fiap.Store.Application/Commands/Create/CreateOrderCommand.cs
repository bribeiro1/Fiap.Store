using Fiap.Store.Application.Dtos;
using MediatR;

namespace Fiap.Store.Application.Commands.Create;


public sealed record CreateOrderCommand(IReadOnlyCollection<OrderItemDto> Items) : IRequest<OrderDto>;