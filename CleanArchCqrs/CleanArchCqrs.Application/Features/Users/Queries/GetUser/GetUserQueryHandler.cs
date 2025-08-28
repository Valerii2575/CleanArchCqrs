using CleanArchCqrs.Domain.Repositories;
using CleanArchCqrs.Domain.ValueObjects;
using CleanArchCqrs.SharedKernel.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchCqrs.Application.Features.Users.Queries.GetUser
{
    public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<GetUserResponse?>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetUserQueryHandler> _logger;

        public GetUserQueryHandler(
            IUserRepository userRepository, 
            ILogger<GetUserQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Result<GetUserResponse?>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(new UserId(request.UserId), cancellationToken);
            if (user is null) 
            {
                _logger.LogWarning($"User with ID {request.UserId} not found");
            }

            var response = new GetUserResponse(user.Id, user.FirstName, user.LastName, user.Email, user.IsActive, user.CreatedAt, user.UpdatedAt);

            return Result<GetUserResponse?>.Success(response);
        }
    }
}
