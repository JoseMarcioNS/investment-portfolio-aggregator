using FluentAssertions;
using InvestmentPortfolio.Domain.Entities;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.Domain.Tests.Entities;

public class TransactionTests
{
    private static Asset CreateAsset() =>
        new(new Ticker("PETR4"), "Petrobras", AssetType.Stock);

    [Fact]
    public void Transaction_WhenCreatedWithValidData_ShouldHaveCorrectProperties()
    {
        var asset = CreateAsset();
        var quantity = new Quantity(100m);
        var price = new Money(30m, "BRL");
        var date = DateTimeOffset.UtcNow;

        var transaction = new Transaction(asset, quantity, price, date, TransactionType.Buy);

        transaction.Id.Should().NotBe(Guid.Empty);
        transaction.Asset.Should().Be(asset);
        transaction.Quantity.Should().Be(quantity);
        transaction.Price.Should().Be(price);
        transaction.Date.Should().Be(date);
        transaction.Type.Should().Be(TransactionType.Buy);
    }

    [Fact]
    public void Transaction_WhenCreatedWithNullAsset_ShouldThrowException()
    {
        var act = () => new Transaction(null!, new Quantity(10m), new Money(10m, "BRL"), DateTimeOffset.UtcNow, TransactionType.Buy);

        act.Should().Throw<DomainException>()
            .WithMessage("*asset*");
    }

    [Fact]
    public void Transaction_WhenCreatedWithFutureDate_ShouldThrowException()
    {
        var act = () => new Transaction(CreateAsset(), new Quantity(10m), new Money(10m, "BRL"), DateTimeOffset.UtcNow.AddDays(1), TransactionType.Buy);

        act.Should().Throw<DomainException>()
            .WithMessage("*date*");
    }

    [Fact]
    public void Transaction_TotalValue_ShouldBeQuantityMultipliedByPrice()
    {
        var transaction = new Transaction(CreateAsset(), new Quantity(100m), new Money(30m, "BRL"), DateTimeOffset.UtcNow, TransactionType.Buy);

        transaction.TotalValue.Should().Be(new Money(3000m, "BRL"));
    }
}
