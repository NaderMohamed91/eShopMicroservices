using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle {Request}- RequestData={RequestData}", typeof(TRequest).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var elapsedTime = timer.Elapsed;
        ////
        /// if the request is greater than 3 seconds, then log the warnings
        if (elapsedTime.Seconds > 3)
            logger.LogWarning("[PERFORMANCE] {Request} request took {TimeTaken} seconds.", typeof(TRequest).Name, elapsedTime.Seconds);

        logger.LogInformation("[END] Handled {Request}", typeof(TRequest).Name);
        return response;
    }
}