using Fiap.Store.Application.Commands.Create;
using Fiap.Store.Domain.Contracts;
using Fiap.Store.Domain.Services;
using FluentValidation.AspNetCore;
using FluentValidation;
using Fiap.Store.Infra.Repositories;
using Fiap.Store.Api.Configurations;
using System.Reflection;

var appAssembly = Assembly.GetExecutingAssembly();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderHandler).Assembly));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(CreateOrderValidator).Assembly);

builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RegisterEndpoints(appAssembly);

app.Run();
