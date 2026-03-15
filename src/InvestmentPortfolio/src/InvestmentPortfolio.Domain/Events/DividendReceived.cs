namespace InvestmentPortfolio.Domain.Events;

public class DividendReceived : DomainEvent
{
    public Guid AssetId { get; }
    public decimal Amount { get; }

    public DividendReceived(Guid assetId, decimal amount) : base(assetId)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

        AssetId = assetId;
        Amount = amount;
    }
}