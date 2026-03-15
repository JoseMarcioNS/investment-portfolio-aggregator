using InvestmentPortfolio.Domain.Events;

namespace InvestmentPortfolio.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; protected set; }

    private readonly List<DomainEvent> _domainEvents = new();

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    protected Entity(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("The id cannot be empty.", nameof(id));

        Id = id;
    }

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;

        return Id == other.Id;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Entity? left, Entity? right)
        => left?.Equals(right) ?? right is null;

    public static bool operator !=(Entity? left, Entity? right)
        => !(left == right);
}
