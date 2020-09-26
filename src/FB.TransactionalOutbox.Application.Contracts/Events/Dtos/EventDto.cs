using System;
using AutoMapper;
using FB.TransactionalOutbox.Domain.EventAggregate;

namespace FB.TransactionalOutbox.Application.Contracts.Events.Dtos
{
    [AutoMap(typeof(Event))]
    public class EventDto
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public object EventBody { get; set; }
        public string TopicName { get; set; }
        public bool IsDeleted { get; set; }
    }
}