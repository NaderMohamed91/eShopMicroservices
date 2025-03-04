namespace Catalog.Api.Features.GetProductById;

public record GetProductByIdResult(Product Product);

public class GetProductByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getProductById/{productId}", async (Guid productId, ISender sender) => 
        {
            var product = await sender.Send(new GetProductByIdQueryRequest(productId));

            var respone = product.Adapt<GetProductByIdResult>();

            return Results.Ok(respone);
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdResult>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}