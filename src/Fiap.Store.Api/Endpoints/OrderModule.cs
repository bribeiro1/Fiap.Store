using Fiap.Store.Api.Configurations;
using Fiap.Store.Application.Commands.Create;
using Fiap.Store.Application.Commands.Update;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Store.Api.Endpoints;

public static class OrderModule
{
    protected class OrderEndpoint : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder endpoints)
        {

            endpoints.MapPost("/orders", async (CreateOrderCommand cmd, IValidator<CreateOrderCommand> validator, IMediator mediator) =>
            {
                var validation = await validator.ValidateAsync(cmd);
                if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());
                var dto = await mediator.Send(cmd);
                return Results.Created($"/orders/{dto.Id}", dto);
            })
            .WithName("CreateOrder")
            .WithOpenApi();

            endpoints.MapPost("/orders/{id}/price", async ([FromRoute] string id, ApplyBestDiscountCommand body, IValidator<ApplyBestDiscountCommand> validator, IMediator mediator) =>
            {
                var cmd = new ApplyBestDiscountCommand(id, body.Coupon);
                var validation = await validator.ValidateAsync(cmd);
                if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());

                try
                {
                    var price = await mediator.Send(cmd);
                    return Results.Ok(new { Final = price });
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound();
                }
                catch (ArgumentException ex) when (ex.ParamName == "coupon" || ex.Message.Contains("Invalid"))
                {
                    return Results.BadRequest("Invalid coupon");
                }
            })
            .WithName("ApplyBestDiscount")
            .WithOpenApi();

        }
    }
}