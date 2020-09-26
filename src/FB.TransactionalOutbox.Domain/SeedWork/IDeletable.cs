using System;

namespace FB.TransactionalOutbox.Domain.SeedWork
{
    public interface IDeletable
    {
        public bool IsDeleted { get;  }
        public DateTime? DeletedTime { get;  }
        public Guid? DeletedUserId { get; }
    }
}