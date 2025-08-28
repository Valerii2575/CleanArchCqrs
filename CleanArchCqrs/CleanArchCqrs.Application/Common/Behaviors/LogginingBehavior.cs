using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CleanArchCqrs.Application.Common.Behaviors
{
    public sealed class LogginingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LogginingBehavior<TRequest, TResponse>> _logger;

        public LogginingBehavior(ILogger<LogginingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var requestId = Guid.NewGuid();
            _logger.LogInformation("Handling {RequestName} with payload: {@Request}", requestName, requestId);

            var stopwatch = Stopwatch.StartNew();
            try 
            {
                _logger.LogInformation("Started handling {RequestName} with RequestId: {RequestId}", requestName, requestId);
                var response = await next();
                stopwatch.Stop();
                _logger.LogInformation("Finished handling {RequestName} with RequestId: {RequestId} in {ElapsedMilliseconds} ms", requestName, requestId, stopwatch.ElapsedMilliseconds);
                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Error handling {RequestName} with RequestId: {RequestId} after {ElapsedMilliseconds} ms", requestName, requestId, stopwatch.ElapsedMilliseconds);
                throw;
            }            
        }
    }
}
