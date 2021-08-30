using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Product.Domain.Common.ValueObjects;

namespace Product.Domain.Common
{
    public abstract class AggregateRoot
    {
        public AggregateRoot()
        {
            Events = new List<DomainEvent>();
        }

        public long Id { get; set; }

        public long CreatedDate { get; set; }

        public long LastModifiedDate { get; set; }

        public long Version { get; set; }

        [JsonIgnore] public List<DomainEvent> Events { get; }

        [JsonIgnore] public bool IsModified { get; private set; }

        protected void AddEvent(string eventName, DomainEventPayload @event)
        {
            if (!IsModified)
                SetAsModified();
            Events.Add(new DomainEvent(eventName, @event));
        }

        protected void SetAsCreated()
        {
            var utcNow = DateTime.UtcNow;
            var timestamp = ((DateTimeOffset) utcNow).ToUnixTimeMilliseconds();
            CreatedDate = timestamp;
            LastModifiedDate = timestamp;
            IsModified = true;
        }

        protected void SetAsModified()
        {
            if (IsModified)
                return;
            var utcNow = DateTime.UtcNow;
            var timestamp = ((DateTimeOffset) utcNow).ToUnixTimeMilliseconds();
            LastModifiedDate = timestamp;
            IsModified = true;
        }
    }
}