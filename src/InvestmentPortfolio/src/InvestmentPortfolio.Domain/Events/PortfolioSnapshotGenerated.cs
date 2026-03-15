namespace InvestmentPortfolio.Domain.Events;

public class PortfolioSnapshotGenerated : DomainEvent
{
    public Guid PortfolioId { get; }
    public decimal TotalValue { get; }

    public PortfolioSnapshotGenerated(Guid portfolioId, decimal totalValue) : base(portfolioId)
    {
        if (totalValue < 0)
            throw new ArgumentException("Total value cannot be negative.", nameof(totalValue));

        PortfolioId = portfolioId;
        TotalValue = totalValue;
    }
}