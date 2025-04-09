namespace Basket.Api.Features.StoreBasket;

public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketCommandResult>;

public record StoreBasketCommandResult(string userName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(a => a.ShoppingCart)
            .NotNull()
            .WithMessage("Shopping cart cannot be null")
            .ChildRules(cart =>
            {
                cart.RuleFor(a => a.UserName).NotNull().WithMessage("UserName is required");
            });
    }
}

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketCommandResult>
{
    public async Task<StoreBasketCommandResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {


        return new StoreBasketCommandResult("nader");
    }
}