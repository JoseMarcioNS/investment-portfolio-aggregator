namespace InvestmentPortfolio.Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("The amount cannot be negative.", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("The currency cannot be null or empty.", nameof(currency));

        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }

    public static Money operator +(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        EnsureSameCurrency(left, right);

        var result = left.Amount - right.Amount;
        if (result < 0)
            throw new InvalidOperationException("Subtraction cannot result in a negative amount.");

        return new Money(result, left.Currency);
    }

    public static Money operator *(Money money, decimal multiplier)
    {
        return new Money(money.Amount * multiplier, money.Currency);
    }

    private static void EnsureSameCurrency(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException(
                $"Cannot operate on different currency: {left.Currency} and {right.Currency}.");
    }
}
