using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.Domain.Entities;

public class Transaction : Entity
{
    public Asset Asset { get; private set; }
    public Quantity Quantity { get; private set; }
    public Money Price { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public TransactionType Type { get; private set; }

    public Money TotalValue => Price * Quantity.Value;

    public Transaction(Asset asset, Quantity quantity, Money price, DateTimeOffset date, TransactionType type)
    {
        if (asset is null)
            throw new DomainException("The asset is required.");

        if (date > DateTimeOffset.UtcNow)
            throw new DomainException("The date cannot be in the future.");

        Asset = asset;
        Quantity = quantity;
        Price = price;
        Date = date;
        Type = type;
    }
}
