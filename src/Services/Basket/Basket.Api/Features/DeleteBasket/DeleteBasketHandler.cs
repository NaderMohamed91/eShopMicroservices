namespace Basket.Api.Features.DeleteBasket;

public record DeleteBasketCommand(string userName) : ICommand<DeleteBasketCommandResult>;

public record DeleteBasketCommandResult(bool isSuccess);

public class DeleteBasketCommandValidators : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidators()
    {
        RuleFor(a => a.userName).NotNull().WithMessage("UserName is required");
    }
}

public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketCommandResult>
{
    public async Task<DeleteBasketCommandResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {


        return new DeleteBasketCommandResult(true);
    }
}