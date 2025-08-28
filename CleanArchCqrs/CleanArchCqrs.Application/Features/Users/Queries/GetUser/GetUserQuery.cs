using CleanArchCqrs.SharedKernel.Common;
using MediatR;

namespace CleanArchCqrs.Application.Features.Users.Queries.GetUser
{
    public sealed record GetUserQuery(Guid UserId) : IRequest<Result<GetUserResponse>>;

    public sealed record GetUserResponse(Guid UserId, 
                                        string FirstName, 
                                        string LastName, 
                                        string Email, 
                                        bool IsActive, 
                                        DateTimeOffset CreatedAt, 
                                        DateTimeOffset? UpdatedAt);
}
