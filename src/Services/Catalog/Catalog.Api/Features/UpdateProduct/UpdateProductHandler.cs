namespace Catalog.Api.Features.UpdateProduct;

public record UpdateProductCommandRequset(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductCommandResult>;

public record UpdateProductCommandResult(bool IsSuccess);

public class UpdateProductCommandRequsetValidator : AbstractValidator<UpdateProductCommandRequset>
{
    public UpdateProductCommandRequsetValidator()
    {
        RuleFor(a => a.Id).NotEmpty().WithMessage("Product Id is required");
        RuleFor(a => a.Name).NotEmpty().WithMessage("Product Name is required")
                            .Length(2, 150).WithMessage("Product Name must be between 2 and 150  characher");
        RuleFor(a => a.Category).NotEmpty().WithMessage("Product Category is required");
        RuleFor(a => a.Description).NotEmpty().WithMessage("Product Description is required");
        RuleFor(a => a.Price).GreaterThan(0).WithMessage("Product Price must be grater than 0");
    }
}

internal class UpdateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommandRequset, UpdateProductCommandResult>
{
    public async Task<UpdateProductCommandResult> Handle(UpdateProductCommandRequset command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null) throw new ProductNotFoundException(command.Id);

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
