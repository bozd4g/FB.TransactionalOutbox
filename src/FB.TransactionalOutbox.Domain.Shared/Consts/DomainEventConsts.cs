namespace FB.TransactionalOutbox.Domain.Shared.Consts
{
    public class DomainEventConsts
    {
        public const string UserCreatedEventExchange = "fb.transactional.outbox.user.created";
        public const string UserCreatedEventQueue = "fb.transactional.outbox.listens.user.created";
        
        public const string EmailChangedEventExchange = "fb.transactional.outbox.user.email.changed";
        public const string EmailChangedEventQueue = "fb.transactional.outbox.listens.user.email.changed";
    }
}