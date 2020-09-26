using FB.TransactionalOutbox.Domain.Shared;
using FB.TransactionalOutbox.Domain.Shared.Consts;
using MediatR;

namespace FB.TransactionalOutbox.Domain.UserAggregate.Events
{
    [Message(DomainEventConsts.UserCreatedEventExchange)]
    public class UserCreatedEvent : INotification
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public UserCreatedEvent(string name, string surname, string email, string password)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
        }
    }
}