
using Catalog.Api.Features.GetProductById;

namespace Catalog.Api.Features.UpdateProduct;

public record UpdateProductRequset(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/updateProduct", async (UpdateProductRequset product, ISender sender) => 
        {
            var productCommandToUpdate = product.Adapt<UpdateProductCommandRequset>();

            var updatedProduct = await sender.Send(productCommandToUpdate);

            var response = updatedProduct.Adapt<UpdateProductResult>();

            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResult>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
