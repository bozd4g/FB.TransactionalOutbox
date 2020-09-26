using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FB.TransactionalOutbox.Domain.EventAggregate;
using FB.TransactionalOutbox.Domain.SeedWork;
using FB.TransactionalOutbox.Domain.Shared;
using FB.TransactionalOutbox.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FB.TransactionalOutbox.EntityFrameworkCore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
                CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEntities = this.ChangeTracker
                                     .Entries<Entity>()
                                     .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                               .SelectMany(x => x.Entity.DomainEvents)
                               .ToList();
            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());
            foreach (var domainEvent in domainEvents)
            {
                var message = MessageAttribute.Get(domainEvent.GetType());
                if (message == null)
                    continue;
                
                var eventBody = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
                {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                });
                Console.WriteLine($"\n------\nA domain event has been published!\n" +
                                  $"Event: {domainEvent.GetType().Name}\n" +
                                  $"TopicName: {message.Name}\n" +
                                  $"EventBody: {eventBody}\n");

                await Events.AddAsync(new Event(domainEvent.GetType().Name, eventBody, message.Name), cancellationToken);
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}