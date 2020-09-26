using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FB.TransactionalOutbox.Application.Contracts.Events;
using FB.TransactionalOutbox.Application.Contracts.Events.Commands;
using FB.TransactionalOutbox.Application.Contracts.Events.Dtos;
using FB.TransactionalOutbox.Application.Contracts.Events.Queries;
using MediatR;

namespace FB.TransactionalOutbox.Application.Events
{
    public class EventsHandler : IRequestHandler<DeleteThrewEventsCommand, bool>,
            IRequestHandler<GetEventsQuery, IList<EventDto>>
    {
        private readonly IEventAppService _eventAppService;

        public EventsHandler(IEventAppService eventAppService)
        {
            _eventAppService = eventAppService;
        }

        public async Task<bool> Handle(DeleteThrewEventsCommand request, CancellationToken cancellationToken)
        {
            return await _eventAppService.Delete(request.Ids);
        }

        public async Task<IList<EventDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            return await _eventAppService.GetEventsAsync(request.IncludeIsDeleted);
        }
    }
}