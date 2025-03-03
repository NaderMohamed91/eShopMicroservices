namespace Catalog.Api.Features.GetProductById;

public record GetProductByIdQuery(Guid productId) : IQuery<GetProductByIdQueryResult>;

public record GetProductByIdQueryResult(Product Product);

internal class GetProductByIdQueryHandler(ILogger<GetProductByIdQueryHandler> logger, IDocumentSession session)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@query}", query);

        var product = await session.LoadAsync<Product>(query.productId, cancellationToken);
        if (product is null) throw new ProductNotFoundException();

        return new GetProductByIdQueryResult(product);
    }
}
