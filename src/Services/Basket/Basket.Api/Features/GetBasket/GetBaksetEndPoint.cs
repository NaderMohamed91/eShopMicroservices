namespace Basket.Api.Features.GetBasket;

public record GetBasketResponse(ShoppingCart ShoppingCart);

public class GetBaksetEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) => 
        {
            var result = sender.Send(new GetBasketQueryRequest(userName));

            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("GetShoppingBasketByUserName")
        .Produces<GetBasketResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Get Shopping Basket By UserName")
        .WithDescription("Get Shopping Basket By UserName");
    }
}