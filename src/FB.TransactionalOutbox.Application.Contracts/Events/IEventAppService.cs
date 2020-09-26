using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FB.TransactionalOutbox.Application.Contracts.Dependency;
using FB.TransactionalOutbox.Application.Contracts.Events.Dtos;

namespace FB.TransactionalOutbox.Application.Contracts.Events
{
    public interface IEventAppService : IInstancePerLifetimeScope
    {
        Task<IList<EventDto>> GetEventsAsync(bool includeIsDeleted = false);
        Task<bool> Delete(IList<Guid> ids);
    }
}