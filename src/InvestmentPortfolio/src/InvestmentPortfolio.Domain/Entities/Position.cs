using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.Domain.Entities;

public class Position : Entity
{
    public Asset Asset { get; private set; }
    public Quantity Quantity { get; private set; }
    public Money AveragePrice { get; private set; }

    public Money TotalValue => AveragePrice * Quantity.Value;

    public Position(Asset asset, Quantity quantity, Money averagePrice)
    {
        if (asset is null)
            throw new DomainException("The asset is required.");

        Asset = asset;
        Quantity = quantity;
        AveragePrice = averagePrice;
    }

    public void Apply(Transaction transaction)
    {
        if (transaction.Type == TransactionType.Buy)
            ApplyBuy(transaction);
        else if (transaction.Type == TransactionType.Sell)
            ApplySell(transaction);
    }

    private void ApplyBuy(Transaction transaction)
    {
        var currentTotal = AveragePrice * Quantity.Value;
        var newTotal = transaction.Price * transaction.Quantity.Value;
        var newQuantity = Quantity + transaction.Quantity;

        AveragePrice = new Money((currentTotal.Amount + newTotal.Amount) / newQuantity.Value, AveragePrice.Currency);
        Quantity = newQuantity;
    }

    private void ApplySell(Transaction transaction)
    {
        if (transaction.Quantity.Value > Quantity.Value)
            throw new DomainException("Cannot sell more than the available quantity.");

        Quantity = Quantity - transaction.Quantity;
    }
}
