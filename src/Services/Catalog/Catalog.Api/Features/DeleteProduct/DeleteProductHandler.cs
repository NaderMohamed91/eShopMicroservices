namespace Catalog.Api.Features.DeleteProduct;

public record DeleteProductCommandRequest(Guid ProductId) : ICommand<DeleteProductCommandResult>;

public record DeleteProductCommandResult(bool IsSuccess);

internal class DeleteProductHandler(ILogger<DeleteProductHandler> logger, IDocumentSession session)
    : ICommandHandler<DeleteProductCommandRequest, DeleteProductCommandResult>
{
    public async Task<DeleteProductCommandResult> Handle(DeleteProductCommandRequest command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductHandler.Handle called with {@command}", command);

        var product = await session.LoadAsync<Product>(command.ProductId, cancellationToken);

        if (product is null) throw new ProductNotFoundException();

        session.Delete(product);
        await session.SaveChangesAsync();

        return new DeleteProductCommandResult(true);
    }
}
