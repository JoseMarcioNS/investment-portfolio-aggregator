using FluentAssertions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.Domain.Tests.ValueObjects;

public class MoneyTests
{
    // --- Creation ---

    [Fact]
    public void Money_WhenCreatedWithValidValues_ShouldHaveCorrectProperties()
    {
        var money = new Money(100.50m, "BRL");

        money.Amount.Should().Be(100.50m);
        money.Currency.Should().Be("BRL");
    }

    [Fact]
    public void Money_WhenCreatedWithNegativeAmount_ShouldThrowException()
    {
        var act = () => new Money(-1m, "BRL");

        act.Should().Throw<ArgumentException>()
            .WithMessage("*amount*");
    }

    [Fact]
    public void Money_WhenCreatedWithNullCurrency_ShouldThrowException()
    {
        var act = () => new Money(100m, null!);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*currency*");
    }

    [Fact]
    public void Money_WhenCreatedWithEmptyCurrency_ShouldThrowException()
    {
        var act = () => new Money(100m, "");

        act.Should().Throw<ArgumentException>()
            .WithMessage("*currency*");
    }

    // --- Equality (Value Object: equality by value) ---

    [Fact]
    public void Money_WhenTwoInstancesHaveSameValues_ShouldBeEqual()
    {
        var money1 = new Money(100m, "BRL");
        var money2 = new Money(100m, "BRL");

        money1.Should().Be(money2);
    }

    [Fact]
    public void Money_WhenTwoInstancesHaveDifferentValues_ShouldNotBeEqual()
    {
        var money1 = new Money(100m, "BRL");
        var money2 = new Money(200m, "BRL");

        money1.Should().NotBe(money2);
    }

    // --- Arithmetic ---

    [Fact]
    public void Money_WhenAdded_ShouldReturnCorrectSum()
    {
        var money1 = new Money(100m, "BRL");
        var money2 = new Money(50m, "BRL");

        var result = money1 + money2;

        result.Amount.Should().Be(150m);
        result.Currency.Should().Be("BRL");
    }

    [Fact]
    public void Money_WhenSubtracted_ShouldReturnCorrectDifference()
    {
        var money1 = new Money(100m, "BRL");
        var money2 = new Money(30m, "BRL");

        var result = money1 - money2;

        result.Amount.Should().Be(70m);
        result.Currency.Should().Be("BRL");
    }

    [Fact]
    public void Money_WhenMultipliedByQuantity_ShouldReturnCorrectValue()
    {
        var money = new Money(50m, "BRL");

        var result = money * 3m;

        result.Amount.Should().Be(150m);
        result.Currency.Should().Be("BRL");
    }

    [Fact]
    public void Money_WhenAddedWithDifferentCurrencies_ShouldThrowException()
    {
        var brl = new Money(100m, "BRL");
        var usd = new Money(100m, "USD");

        var act = () => brl + usd;

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*currency*");
    }

    [Fact]
    public void Money_WhenSubtractedResultsInNegative_ShouldThrowException()
    {
        var money1 = new Money(30m, "BRL");
        var money2 = new Money(100m, "BRL");

        var act = () => money1 - money2;

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*negative*");
    }
}
