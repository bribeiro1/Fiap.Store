namespace Fiap.Store.Api.Configurations;

public interface IEndpoint
{
    static abstract void MapEndpoint(IEndpointRouteBuilder endpoints);
}