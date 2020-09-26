using AutoMapper;
using FB.TransactionalOutbox.Domain.UserAggregate;

namespace FB.TransactionalOutbox.Application.Contracts.Users.Dtos
{
    [AutoMap(typeof(User))]
    public class UserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string TempEmail { get; set; }
        public int Age { get; set; }
    }
}