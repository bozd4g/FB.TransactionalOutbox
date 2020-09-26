using FB.TransactionalOutbox.Domain.Shared;
using FB.TransactionalOutbox.Domain.Shared.Consts;
using MediatR;

namespace FB.TransactionalOutbox.Domain.UserAggregate.Events
{
    [Message(DomainEventConsts.EmailChangedEventExchange)]
    public class EmailChangedEvent : INotification
    {
        public string Email { get; }

        public EmailChangedEvent(string email)
        {
            Email = email;
        }
    }
}