namespace Catalog.Api.Features.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getProductByCategory/{category}", async (string category, ISender sender) => 
        {
            var products = await sender.Send(new GetProductByCategoryQueryRequest(category));

            var response = products.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
    }
}