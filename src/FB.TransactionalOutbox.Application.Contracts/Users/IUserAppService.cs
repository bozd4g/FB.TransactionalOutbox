using System.Threading.Tasks;
using FB.TransactionalOutbox.Application.Contracts.Dependency;
using FB.TransactionalOutbox.Application.Contracts.Users.Commands;
using FB.TransactionalOutbox.Application.Contracts.Users.Dtos;
using FB.TransactionalOutbox.Application.Contracts.Users.Queries;

namespace FB.TransactionalOutbox.Application.Contracts.Users
{
    public interface IUserAppService : IInstancePerLifetimeScope
    {
        Task<UserDto> GetUser(GetUserByEmailQuery byEmailQuery);

        Task<UserDto> CreateUser(CreateUserCommand command);

        Task<UserDto> ChangeEmail(ChangeEmailCommand command);
    }
}