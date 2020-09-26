using FB.TransactionalOutbox.Application.Contracts.Users.Dtos;
using MediatR;

namespace FB.TransactionalOutbox.Application.Contracts.Users.Queries
{
    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        public string Email { get; private set; }

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}