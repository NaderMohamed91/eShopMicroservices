
using Catalog.Api.Features.CreateProduct;

namespace Catalog.Api.Features.GetProducts;

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) => 
        {
            var products = await sender.Send(new GetProductsQuery());

            var response = products.Adapt<GetProductsResult>();

            return Results.Ok(response);
        })
        .WithName("GetProduct")
        .Produces<GetProductsEndPoint>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product")
        .WithDescription("Get Product");
    }
}