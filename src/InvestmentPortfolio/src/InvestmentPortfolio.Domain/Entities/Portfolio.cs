using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.Domain.Entities;

public class Portfolio : Entity
{
    private readonly List<Position> _positions = [];

    public BrokerAccount BrokerAccount { get; private set; }
    public IReadOnlyCollection<Position> Positions => _positions.AsReadOnly();

    public Money TotalValue => _positions
        .Aggregate(
            new Money(0m, "BRL"),
            (total, position) => total + position.TotalValue);

    public Portfolio(BrokerAccount brokerAccount)
    {
        if (brokerAccount is null)
            throw new DomainException("The brokerAccount is required.");

        BrokerAccount = brokerAccount;
    }

    public void AddPosition(Position position)
    {
        var exists = _positions.Any(p => p.Asset.Ticker == position.Asset.Ticker);
        if (exists)
            throw new DomainException($"Position for {position.Asset.Ticker} already exists in this portfolio.");

        _positions.Add(position);
    }

    public Position? GetPositionByTicker(Ticker ticker)
        => _positions.FirstOrDefault(p => p.Asset.Ticker == ticker);
}
