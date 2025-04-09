namespace Basket.Api.Features.GetBasket;

public record GetBasketQueryRequest(string userName) : IQuery<GetBasketQueryResult>;

public record GetBasketQueryResult(ShoppingCart ShoppingCart);

public class GetBasketResquestHandler : IRequestHandler<GetBasketQueryRequest, GetBasketQueryResult>
{
    public async Task<GetBasketQueryResult> Handle(GetBasketQueryRequest query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}