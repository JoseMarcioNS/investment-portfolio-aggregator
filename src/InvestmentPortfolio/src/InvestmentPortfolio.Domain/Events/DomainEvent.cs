namespace InvestmentPortfolio.Domain.Events;

public abstract class DomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
    public Guid AggregateId { get; }

    protected DomainEvent(Guid aggregateId)
    {
        if (aggregateId == Guid.Empty)
            throw new ArgumentException("AggregateId cannot be empty.", nameof(aggregateId));

        AggregateId = aggregateId;
    }
}