using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FB.TransactionalOutbox.Application;
using FB.TransactionalOutbox.Application.Contracts.Users.Commands;
using FB.TransactionalOutbox.Application.Contracts.Users.Dtos.Request;
using FB.TransactionalOutbox.Application.Contracts.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FB.TransactionalOutbox.Api.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet, Route("{email}")]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
        public async Task<ApiResponse> Get(string email = "furkan@bozdag.dev")
        {
            var response = await _mediator.Send(new GetUserByEmailQuery(email));
            return ApiResponse(response);
        }

        [HttpPost, Route("")]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
        public async Task<ApiResponse> Create([FromBody] CreateUserRequestDto request)
        {
            var command = _mapper.Map<CreateUserCommand>(request);
            await _mediator.Send(command);

            return ApiResponse();
        }

        [HttpPut, Route("change-email")]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
        public async Task<ApiResponse> ChangeEmail([FromBody] ChangeEmailRequestDto request)
        {
            var command = _mapper.Map<ChangeEmailCommand>(request);
            await _mediator.Send(command);

            return ApiResponse();
        }
    }
}