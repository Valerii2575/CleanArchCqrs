using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchCqrs.Application.Common.Behaviors
{
    public sealed class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
        private const int LongRunningRequestThreshold = 500; // milliseconds

        public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var requestId = Guid.NewGuid();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var response = await next();
            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds > LongRunningRequestThreshold)
            {
                _logger.LogWarning("Long Running Request: {RequestName} with RequestId: {RequestId} took {ElapsedMilliseconds} ms", requestName, requestId, stopwatch.ElapsedMilliseconds);
            }
            else
            {
                _logger.LogInformation("Handled {RequestName} with RequestId: {RequestId} in {ElapsedMilliseconds} ms", requestName, requestId, stopwatch.ElapsedMilliseconds);
            }
            return response;
        }
    }
}
