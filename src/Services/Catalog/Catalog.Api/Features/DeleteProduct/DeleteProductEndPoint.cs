namespace Catalog.Api.Features.DeleteProduct;

public record DeleteProductResult(bool IsSuccess);

public class DeleteProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/deleteProduct/{productId}", async (Guid productId, ISender sender) => 
        {
            var response = await sender.Send(new DeleteProductCommandRequest(productId));

            var result = response.Adapt(response);

            return Results.Ok(result);
        })
        .WithName("DeleteProduct")
        .Produces<DeleteProductResult>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product");
    }
}
