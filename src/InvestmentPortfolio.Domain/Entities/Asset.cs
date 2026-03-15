using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.Domain.Entities;

public class Asset : Entity
{
    public Ticker Ticker { get; private set; }
    public string Name { get; private set; }
    public AssetType Type { get; private set; }

    public Asset(Ticker ticker, string name, AssetType type)
    {
        if (ticker is null)
            throw new DomainException("The ticker is required.");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("The name is required.");

        Ticker = ticker;
        Name = name;
        Type = type;
    }
}
