namespace Catalog.Api.Features.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResults>;

public record GetProductsResults(IEnumerable<Product> Products);


internal class GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger, IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResults>
{
    public async Task<GetProductsResults> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@query}", query);

        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductsResults(products);
    }
}