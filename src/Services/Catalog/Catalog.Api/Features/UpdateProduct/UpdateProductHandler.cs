namespace Catalog.Api.Features.UpdateProduct;

public record UpdateProductCommandRequset(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductCommandResult>;

public record UpdateProductCommandResult(bool IsSuccess);

internal class UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger, IDocumentSession session)
    : ICommandHandler<UpdateProductCommandRequset, UpdateProductCommandResult>
{
    public async Task<UpdateProductCommandResult> Handle(UpdateProductCommandRequset command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called with {@command}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null) throw new ProductNotFoundException();

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);
        await session.SaveChangesAsync();

        return new UpdateProductCommandResult(true);
    }
}
