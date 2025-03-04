namespace Catalog.Api.Features.GetProductById;

public record GetProductByIdQueryRequest(Guid productId) : IQuery<GetProductByIdQueryResult>;

public record GetProductByIdQueryResult(Product Product);

public class GetProductByIdQueryRequestValidator : AbstractValidator<GetProductByIdQueryRequest>
{
    public GetProductByIdQueryRequestValidator()
    {
        RuleFor(a => a.productId).NotEmpty().WithMessage("Product Id is required");
    }
}

internal class GetProductByIdQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByIdQueryRequest, GetProductByIdQueryResult>
{
    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQueryRequest query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.productId, cancellationToken);
        if (product is null) throw new ProductNotFoundException(query.productId);

        return new GetProductByIdQueryResult(product);
    }
}
