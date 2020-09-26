using System.Threading.Tasks;
using AutoMapper;
using FB.TransactionalOutbox.Application.Contracts;
using FB.TransactionalOutbox.Application.Contracts.Users;
using FB.TransactionalOutbox.Application.Contracts.Users.Commands;
using FB.TransactionalOutbox.Application.Contracts.Users.Dtos;
using FB.TransactionalOutbox.Application.Contracts.Users.Queries;
using FB.TransactionalOutbox.Application.Exceptions;
using FB.TransactionalOutbox.Domain.UserAggregate;
using FB.TransactionalOutbox.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FB.TransactionalOutbox.Application.Users
{
    public class UserAppService : BaseAppService, IUserAppService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserAppService(IMapper mapper, ILogger<UserAppService> logger,
                ApplicationDbContext dbContext) : base(mapper, logger)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDto> GetUser(GetUserByEmailQuery byEmailQuery)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == byEmailQuery.Email);
            if (user == null)
                throw new UserFriendlyException("User does not exist!");

            var mappedUser = Mapper.Map<UserDto>(user);

            user.RetrieveDetails();
            mappedUser.Age = user.Detail.Age;
            return mappedUser;
        }

        public async Task<UserDto> CreateUser(CreateUserCommand command)
        {
            var user = new User(command.Name, command.Surname, command.Email, command.Password, command.Birthday);
            var inserted = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return Mapper.Map<UserDto>(inserted.Entity);
        }

        public async Task<UserDto> ChangeEmail(ChangeEmailCommand command)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == command.UserId);
            if (user == null)
                throw new UserFriendlyException("The user does not found!");

            user.ChangeEmail(command.Email);
            await _dbContext.SaveChangesAsync();

            return Mapper.Map<UserDto>(user);
        }
    }
}