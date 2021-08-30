namespace Product.Domain.Common.ValueObjects
{
    public abstract class DomainEventPayload
    {
        public long AggregateId { get; private set; }

        public long Timestamp { get; private set; }

        public long Version { get; private set; }

        public void SetMetadata(long aggregateId, long timestamp, long version)
        {
            AggregateId = aggregateId;
            Timestamp = timestamp;
            Version = version;
        }
    }
}