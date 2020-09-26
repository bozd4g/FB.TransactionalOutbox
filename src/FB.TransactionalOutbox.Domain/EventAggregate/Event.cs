using System;
using System.ComponentModel.DataAnnotations.Schema;
using FB.TransactionalOutbox.Domain.SeedWork;

namespace FB.TransactionalOutbox.Domain.EventAggregate
{
    public class Event : Entity, IAggregateRoot, IDeletable
    {
        public string EventName { get; private set; }
        public string TopicName { get; private set; }

        [Column(TypeName = "jsonb")]
        public string EventBody { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime? DeletedTime { get; private set; }
        public Guid? DeletedUserId { get; private set; }

        public Event()
        {
        }

        public Event(string eventName, string eventBody, string topicName, Guid creatorUserId = default)
        {
            EventName = eventName;
            EventBody = eventBody;
            TopicName = topicName;

            CreatorUserId = creatorUserId;
            CreationTime = DateTime.Now;
            LastModifierUserId = creatorUserId;
            LastModificationTime = DateTime.Now;
        }

        public void Delete(Guid deletedUserId = default)
        {
            IsDeleted = true;
            DeletedTime = DateTime.Now;
            DeletedUserId = deletedUserId;
            LastModifierUserId = deletedUserId;
            LastModificationTime = DateTime.Now;
        }
    }
}