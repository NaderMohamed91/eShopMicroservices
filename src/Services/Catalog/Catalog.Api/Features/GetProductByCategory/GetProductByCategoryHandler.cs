namespace Catalog.Api.Features.GetProductByCategory;

public record GetProductByCategoryQueryRequest(string category) : IQuery<GetProductByCategoryQueryResult>;

public record GetProductByCategoryQueryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(ILogger<GetProductByCategoryQueryHandler> logger, IDocumentSession session)
    : IQueryHandler<GetProductByCategoryQueryRequest, GetProductByCategoryQueryResult>
{
    public async Task<GetProductByCategoryQueryResult> Handle(GetProductByCategoryQueryRequest query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@query}", query);

        var products = await session.Query<Product>().Where(a => a.Category.Contains(query.category)).ToListAsync();

        return new GetProductByCategoryQueryResult(products);
    }
}
