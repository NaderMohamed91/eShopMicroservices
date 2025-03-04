namespace Catalog.Api.Features.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(a => a.Name).NotEmpty().WithMessage("Product Name is required")
                            .Length(2, 150).WithMessage("Product Name must be between 2 and 150  characher");
        RuleFor(a => a.Category).NotEmpty().WithMessage("Product Category is required");
        RuleFor(a => a.Description).NotEmpty().WithMessage("Product Description is required");
        RuleFor(a => a.Price).GreaterThan(0).WithMessage("Product Price must be grater than 0");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            //Id = Guid.NewGuid(),
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        //var createProductCommand = command.Adapt<Product>();

        session.Store(product);
        await session.SaveChangesAsync();

        return new CreateProductResult(product.Id);
    }
}
