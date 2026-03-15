using FluentAssertions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.Domain.Tests.ValueObjects;

public class QuantityTests
{
    // --- Creation ---

    [Fact]
    public void Quantity_WhenCreatedWithValidValue_ShouldHaveCorrectValue()
    {
        var quantity = new Quantity(10.5m);

        quantity.Value.Should().Be(10.5m);
    }

    [Fact]
    public void Quantity_WhenCreatedWithZero_ShouldBeValid()
    {
        var quantity = new Quantity(0m);

        quantity.Value.Should().Be(0m);
    }

    [Fact]
    public void Quantity_WhenCreatedWithNegativeValue_ShouldThrowException()
    {
        var act = () => new Quantity(-1m);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*value*");
    }

    // --- Arithmetic ---

    [Fact]
    public void Quantity_WhenAdded_ShouldReturnCorrectSum()
    {
        var q1 = new Quantity(10m);
        var q2 = new Quantity(5m);

        var result = q1 + q2;

        result.Value.Should().Be(15m);
    }

    [Fact]
    public void Quantity_WhenSubtracted_ShouldReturnCorrectDifference()
    {
        var q1 = new Quantity(10m);
        var q2 = new Quantity(4m);

        var result = q1 - q2;

        result.Value.Should().Be(6m);
    }

    [Fact]
    public void Quantity_WhenSubtractedResultsInNegative_ShouldThrowException()
    {
        var q1 = new Quantity(4m);
        var q2 = new Quantity(10m);

        var act = () => q1 - q2;

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*negative*");
    }

    // --- Equality ---

    [Fact]
    public void Quantity_WhenTwoInstancesHaveSameValue_ShouldBeEqual()
    {
        var q1 = new Quantity(10m);
        var q2 = new Quantity(10m);

        q1.Should().Be(q2);
    }

    [Fact]
    public void Quantity_WhenTwoInstancesHaveDifferentValues_ShouldNotBeEqual()
    {
        var q1 = new Quantity(10m);
        var q2 = new Quantity(20m);

        q1.Should().NotBe(q2);
    }
}
