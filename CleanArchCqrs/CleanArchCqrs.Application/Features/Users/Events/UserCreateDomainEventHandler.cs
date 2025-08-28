using CleanArchCqrs.Application.Common.Interfaces;
using CleanArchCqrs.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchCqrs.Application.Features.Users.Events
{
    public sealed class UserCreateDomainEventHandler //: INotificationHandler<UserCreateDomainEvents>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<UserCreateDomainEventHandler> _logger;

        public UserCreateDomainEventHandler(IEmailService emailService, ILogger<UserCreateDomainEventHandler> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Handle(UserCreateDomainEvents notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling UserCreated domain event for user {notification.UserId}");

            try
            {
                await _emailService.SendWelcomeEmailAsync(notification.Email, notification.FullName, cancellationToken);
                _logger.LogInformation($"Wlcome email sent successfullyfor user {notification.UserId}");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"Failed to send welcome email for user {notification.UserId}");
            }
        }
    }
}
