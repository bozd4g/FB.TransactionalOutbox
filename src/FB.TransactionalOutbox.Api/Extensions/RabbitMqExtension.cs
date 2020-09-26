using FB.TransactionalOutbox.Domain.Shared.Consts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace FB.TransactionalOutbox.Api.Extensions
{
    public static class RabbitMqExtension
    {
        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            string hostName = configuration.GetValue<string>("RabbitMq:Url");
            string virtualHost = configuration.GetValue<string>("RabbitMq:VirtualHost");
            string username = configuration.GetValue<string>("RabbitMq:Username");
            string password = configuration.GetValue<string>("RabbitMq:Password");

            var factory = new ConnectionFactory()
            {
                    HostName = hostName,
                    VirtualHost = virtualHost,
                    UserName = username,
                    Password = password
            };
            IModel channel = factory.CreateConnection().CreateModel();
            services.AddSingleton(channel);

            channel.DeclareExchangeAndQueue(DomainEventConsts.UserCreatedEventExchange, DomainEventConsts.UserCreatedEventQueue);
            channel.DeclareExchangeAndQueue(DomainEventConsts.EmailChangedEventExchange, DomainEventConsts.EmailChangedEventQueue);
        }

        private static void DeclareExchangeAndQueue(this IModel channel, string exchange, string queue,
                string exchangeType = "fanout", string routing = "", 
                bool durable = true, bool exclusive = false, bool autoDelete = false)
        {
            channel.QueueDeclare(queue, durable, exclusive, autoDelete);
            channel.ExchangeDeclare(exchange, exchangeType);
            channel.QueueBind(queue, exchange, routing);
        }
    }
}