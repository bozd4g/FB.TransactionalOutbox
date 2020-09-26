using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FB.TransactionalOutbox.Application;
using FB.TransactionalOutbox.Application.Contracts.Events.Queries;
using FB.TransactionalOutbox.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FB.TransactionalOutbox.Api.Controllers
{
    public class EventsController : ApiController
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("")]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
        public async Task<ApiResponse> Get()
        {
            var response = await _mediator.Send(new GetEventsQuery(includeIsDeleted: true));
            return ApiResponse(response.ToList());
        }
    }
}