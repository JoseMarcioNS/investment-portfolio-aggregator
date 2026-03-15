using FluentAssertions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.Domain.Tests.ValueObjects;

public class TickerTests
{
    // --- Creation ---

    [Fact]
    public void Ticker_WhenCreatedWithValidSymbol_ShouldHaveCorrectValue()
    {
        var ticker = new Ticker("PETR4");

        ticker.Symbol.Should().Be("PETR4");
    }

    [Theory]
    [InlineData("petr4")]
    [InlineData("aapl")]
    [InlineData("btc")]
    public void Ticker_WhenCreatedWithLowercaseSymbol_ShouldNormalizeToUppercase(string symbol)
    {
        var ticker = new Ticker(symbol);

        ticker.Symbol.Should().Be(symbol.ToUpperInvariant());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Ticker_WhenCreatedWithNullOrEmptySymbol_ShouldThrowException(string? symbol)
    {
        var act = () => new Ticker(symbol!);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*symbol*");
    }

    [Fact]
    public void Ticker_WhenCreatedWithSymbolTooLong_ShouldThrowException()
    {
        var act = () => new Ticker("TOOLONGSYMBOL");

        act.Should().Throw<ArgumentException>()
            .WithMessage("*symbol*");
    }

    [Theory]
    [InlineData("PETR@4")]
    [InlineData("BTC-USD")]
    [InlineData("AAPL!")]
    public void Ticker_WhenCreatedWithInvalidCharacters_ShouldThrowException(string symbol)
    {
        var act = () => new Ticker(symbol);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*symbol*");
    }

    // --- Equality (Value Object: equality by value) ---

    [Fact]
    public void Ticker_WhenTwoInstancesHaveSameSymbol_ShouldBeEqual()
    {
        var ticker1 = new Ticker("PETR4");
        var ticker2 = new Ticker("PETR4");

        ticker1.Should().Be(ticker2);
    }

    [Fact]
    public void Ticker_WhenTwoInstancesHaveDifferentSymbols_ShouldNotBeEqual()
    {
        var ticker1 = new Ticker("PETR4");
        var ticker2 = new Ticker("VALE3");

        ticker1.Should().NotBe(ticker2);
    }

    [Fact]
    public void Ticker_WhenConvertedToString_ShouldReturnSymbol()
    {
        var ticker = new Ticker("AAPL");

        ticker.ToString().Should().Be("AAPL");
    }
}
