using Marten.Pagination;

public record GetProductsQuery(int? PageNumber, int? PageSize) : IQuery<GetProductsResults>;

public record GetProductsResults(IEnumerable<Product> Products);


internal class GetProductsQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResults>
{
    public async Task<GetProductsResults> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session
                                .Query<Product>()
                                .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetProductsResults(products);
    }
}