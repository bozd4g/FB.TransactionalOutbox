using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FB.TransactionalOutbox.Application.Contracts.Events.Commands;
using FB.TransactionalOutbox.Application.Contracts.Events.Queries;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FB.TransactionalOutbox.Infrastructure.Hangfire.Jobs
{
    public class OutboxTableReadJob : IJob
    {
        private readonly IMediator _mediator;
        private readonly IModel _channel;

        public OutboxTableReadJob(IMediator mediator, IModel channel)
        {
            _mediator = mediator;
            _channel = channel;
        }

        public async Task Start()
        {
            var events = await _mediator.Send(new GetEventsQuery());
            if (events.Count == 0)
                return;

            foreach (var @event in events)
            {
                _channel.BasicPublish(routingKey: "", basicProperties: _channel.CreateBasicProperties(),
                        exchange: @event.TopicName,
                        body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event.EventBody)));
            }

            await _mediator.Send(new DeleteThrewEventsCommand(events.Select(e => e.Id).ToList()));
        }
    }
}