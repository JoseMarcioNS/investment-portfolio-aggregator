using System.Text.RegularExpressions;

namespace InvestmentPortfolio.Domain.ValueObjects;

public record Ticker
{
    private const int MaxLength = 10;
    private static readonly Regex ValidSymbolPattern = new(@"^[A-Z0-9]+$", RegexOptions.Compiled);

    public string Symbol { get; }

    public Ticker(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("The symbol cannot be null or empty.", nameof(symbol));

        var normalized = symbol.ToUpperInvariant().Trim();

        if (normalized.Length > MaxLength)
            throw new ArgumentException(
                $"The symbol cannot exceed {MaxLength} characters.", nameof(symbol));

        if (!ValidSymbolPattern.IsMatch(normalized))
            throw new ArgumentException(
                "The symbol can only contain letters and numbers.", nameof(symbol));

        Symbol = normalized;
    }

    public override string ToString() => Symbol;
}
