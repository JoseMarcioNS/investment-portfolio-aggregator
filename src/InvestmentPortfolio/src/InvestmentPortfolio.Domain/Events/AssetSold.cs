namespace InvestmentPortfolio.Domain.Events;

public class AssetSold : DomainEvent
{
    public Guid AssetId { get; }
    public decimal Quantity { get; }
    public decimal Price { get; }

    public AssetSold(Guid assetId, decimal quantity, decimal price) : base(assetId)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero.", nameof(price));

        AssetId = assetId;
        Quantity = quantity;
        Price = price;
    }
}