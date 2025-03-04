namespace Catalog.Api.Features.DeleteProduct;

public record DeleteProductCommandRequest(Guid ProductId) : ICommand<DeleteProductCommandResult>;

public record DeleteProductCommandResult(bool IsSuccess);

public class DeleteProductCommandRequestValidator : AbstractValidator<DeleteProductCommandRequest>
{
    public DeleteProductCommandRequestValidator()
    {
        RuleFor(a => a.ProductId).NotEmpty().WithMessage("Product Id is required");
    }
}

internal class DeleteProductHandler(IDocumentSession session)
    : ICommandHandler<DeleteProductCommandRequest, DeleteProductCommandResult>
{
    public async Task<DeleteProductCommandResult> Handle(DeleteProductCommandRequest command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.ProductId, cancellationToken);

        if (product is null) throw new ProductNotFoundException(command.ProductId);

        session.Delete(product);
        await session.SaveChangesAsync();

        return new DeleteProductCommandResult(true);
    }
}
