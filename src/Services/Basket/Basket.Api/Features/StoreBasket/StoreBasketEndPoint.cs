namespace Basket.Api.Features.StoreBasket;

public record StoreBasketRequest(ShoppingCart ShoppingCart);

public record StoreBasketResponse(string userName);

public class StoreBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/add", async (StoreBasketRequest request, ISender sender) => 
        {
            var command = request.Adapt<StoreBasketCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<StoreBasketResponse>();

            return Results.Created($"/basket/{response.userName}", response);
        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Store shopping cart basket")
        .WithDescription("Store shopping cart basket");
    }
}