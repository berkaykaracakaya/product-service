namespace Product.Domain.Common.ValueObjects
{
    public class DomainEvent
    {
        public DomainEvent(string eventName, DomainEventPayload payload)
        {
            EventName = eventName;
            Payload = payload;
        }

        public DomainEvent()
        {
        }

        public string EventName { get; set; }

        public DomainEventPayload Payload { get; set; }
    }
}