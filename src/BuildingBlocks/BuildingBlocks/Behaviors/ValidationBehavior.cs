using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators) /*, IValidator<TRequest> validator*/
    : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(validators.Select(a => a.ValidateAsync(context, cancellationToken)));
        var failure = validationResults.Where(a => a.Errors.Any()).SelectMany(er => er.Errors).ToList();

        //var validationResults = await validator.ValidateAsync(context, cancellationToken);
        //var failure = validationResults.Errors.ToList();

        if (failure.Any()) throw new ValidationException(failure);

        return await next();
    }
}