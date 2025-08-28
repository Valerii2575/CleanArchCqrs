using CleanArchCqrs.Domain.Entities;
using CleanArchCqrs.Domain.Repositories;
using CleanArchCqrs.SharedKernel.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchCqrs.Application.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<CreateUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRepository userRepository, 
                                        IUnitOfWork unitOfWork, 
                                        ILogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if(existingUser is not null)
            {
                var error = new Error("User.EmailExists", $"User with email {request.Email} already exists.");
                return (Result<CreateUserResponse>)Result<CreateUserResponse>.Failure(error);
            }

            var userResult = User.Create(request.FirstName, request.LastName, request.Email);
            if (userResult.IsFailure)
            {
                return (Result<CreateUserResponse>)Result<CreateUserResponse>.Failure(userResult.Error);
            }

            var user = userResult
                .Value;
            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("User created with ID: {UserId}, Email: {Email}", user.Id, user.Email);
            return Result<CreateUserResponse>.Success(new CreateUserResponse(user.Id, user.FirstName, user.LastName, user.Email.Value, user.CreatedAt));
        }
    }
}
