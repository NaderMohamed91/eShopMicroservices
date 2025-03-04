namespace Catalog.Api.Features.GetProductByCategory;

public record GetProductByCategoryQueryRequest(string category) : IQuery<GetProductByCategoryQueryResult>;

public record GetProductByCategoryQueryResult(IEnumerable<Product> Products);

public class GetProductByCategoryQueryRequestValidator : AbstractValidator<GetProductByCategoryQueryRequest>
{
    public GetProductByCategoryQueryRequestValidator()
    {
        RuleFor(a => a.category).NotEmpty().WithMessage("Product Category is required");
    }
}

internal class GetProductByCategoryQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByCategoryQueryRequest, GetProductByCategoryQueryResult>
{
    public async Task<GetProductByCategoryQueryResult> Handle(GetProductByCategoryQueryRequest query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().Where(a => a.Category.Contains(query.category)).ToListAsync();

        return new GetProductByCategoryQueryResult(products);
    }
}
