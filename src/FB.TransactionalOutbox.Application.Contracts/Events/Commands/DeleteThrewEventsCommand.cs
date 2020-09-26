using System;
using System.Collections.Generic;
using MediatR;

namespace FB.TransactionalOutbox.Application.Contracts.Events.Commands
{
    public class DeleteThrewEventsCommand : IRequest<bool>
    {
        public IList<Guid> Ids { get; }

        public DeleteThrewEventsCommand(IList<Guid> ids)
        {
            Ids = ids;
        }
    }
}