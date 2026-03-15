using InvestmentPortfolio.Domain.Events;

namespace InvestmentPortfolio.Domain.Services;

public interface IDomainEventHandler<in TEvent> where TEvent : DomainEvent
{
    Task HandleAsync(TEvent domainEvent);
}

public class DomainEventDispatcher
{
    private readonly IEnumerable<IDomainEventHandler<DomainEvent>> _handlers;

    public DomainEventDispatcher(IEnumerable<IDomainEventHandler<DomainEvent>> handlers)
    {
        _handlers = handlers;
    }

    public async Task DispatchAsync(IEnumerable<DomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            var handlers = _handlers.Where(h => h.GetType().GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericArguments().Contains(domainEvent.GetType())));

            foreach (var handler in handlers)
            {
                await ((dynamic)handler).HandleAsync((dynamic)domainEvent);
            }
        }
    }
}