using CleanArchCqrs.SharedKernel.Common;
using MediatR;

namespace CleanArchCqrs.Application.Features.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(string FirstName, string LastName, string Email) : IRequest<Result<CreateUserResponse>>
    {
    }

    public sealed record CreateUserResponse(Guid UserId, string FirstName, string LastName, string Email, DateTimeOffset CreatedAt)
    {
    }
}
