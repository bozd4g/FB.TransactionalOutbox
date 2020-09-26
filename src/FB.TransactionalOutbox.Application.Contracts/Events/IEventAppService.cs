using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FB.TransactionalOutbox.Application.Contracts.Dependency;
using FB.TransactionalOutbox.Application.Contracts.Events.Dtos;

namespace FB.TransactionalOutbox.Application.Contracts.Events
{
    public interface IEventAppService : IInstancePerLifetimeScope
    {
        Task<IList<EventDto>> GetUndeletedEventsAsync();
        Task<IList<EventDto>> GetEventsAsync();
        Task<bool> Delete(IList<Guid> ids);
    }
}