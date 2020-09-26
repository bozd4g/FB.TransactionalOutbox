using System.Collections.Generic;
using FB.TransactionalOutbox.Application.Contracts.Events.Dtos;
using MediatR;

namespace FB.TransactionalOutbox.Application.Contracts.Events.Queries
{
    public class GetUndeletedEventsQuery : IRequest<IList<EventDto>>
    {
    }
}