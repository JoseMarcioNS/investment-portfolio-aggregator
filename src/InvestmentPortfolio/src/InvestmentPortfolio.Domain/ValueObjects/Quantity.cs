namespace InvestmentPortfolio.Domain.ValueObjects;

public record Quantity
{
    public decimal Value { get; }

    public Quantity(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("The value cannot be negative.", nameof(value));

        Value = value;
    }

    public static Quantity operator +(Quantity left, Quantity right)
        => new(left.Value + right.Value);

    public static Quantity operator -(Quantity left, Quantity right)
    {
        var result = left.Value - right.Value;
        if (result < 0)
            throw new InvalidOperationException("Subtraction cannot result in a negative quantity.");

        return new Quantity(result);
    }
}
