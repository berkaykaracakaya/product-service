using System;
using System.Text.Json;
using Product.Domain.Common.ValueObjects;

namespace Product.Infrastructure.Common.ValueObjects
{
    public class OutboxMessage
    {
        public OutboxMessage()
        {
        }

        public OutboxMessage(DomainEvent @event)
        {
            Id = Guid.NewGuid();
            EventName = @event.EventName;
            AggregateId = @event.Payload.AggregateId;
            Version = @event.Payload.Version;
            Timestamp = @event.Payload.Timestamp;
            Event = JsonSerializer.Serialize((object) @event.Payload, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            IsSent = false;
            SentDate = new long?();
        }

        public Guid Id { get; set; }

        public string EventName { get; set; }

        public long AggregateId { get; set; }

        public long Version { get; set; }

        public string Event { get; set; }

        public long Timestamp { get; set; }

        public bool IsSent { get; set; }

        public long? SentDate { get; set; }
    }
}