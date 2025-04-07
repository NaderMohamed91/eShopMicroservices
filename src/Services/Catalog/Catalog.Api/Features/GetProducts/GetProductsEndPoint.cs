namespace Catalog.Api.Features.GetProducts;

public record GetProductsRequest(int? PageNumber, int? PageSize);

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request ,ISender sender) => 
        {
            var query = request.Adapt<GetProductsQuery>();

            var products = await sender.Send(query);

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