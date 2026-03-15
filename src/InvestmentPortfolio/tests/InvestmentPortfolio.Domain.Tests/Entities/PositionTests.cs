using FluentAssertions;
using InvestmentPortfolio.Domain.Entities;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.Domain.Tests.Entities;

public class PositionTests
{
    private static Asset CreateAsset() =>
        new(new Ticker("PETR4"), "Petrobras", AssetType.Stock);

    [Fact]
    public void Position_WhenCreatedWithValidData_ShouldHaveCorrectProperties()
    {
        var asset = CreateAsset();
        var position = new Position(asset, new Quantity(100m), new Money(30m, "BRL"));

        position.Id.Should().NotBe(Guid.Empty);
        position.Asset.Should().Be(asset);
        position.Quantity.Should().Be(new Quantity(100m));
        position.AveragePrice.Should().Be(new Money(30m, "BRL"));
    }

    [Fact]
    public void Position_WhenCreatedWithNullAsset_ShouldThrowException()
    {
        var act = () => new Position(null!, new Quantity(100m), new Money(30m, "BRL"));

        act.Should().Throw<DomainException>()
            .WithMessage("*asset*");
    }

    [Fact]
    public void Position_TotalValue_ShouldBeQuantityMultipliedByAveragePrice()
    {
        var position = new Position(CreateAsset(), new Quantity(100m), new Money(30m, "BRL"));

        position.TotalValue.Should().Be(new Money(3000m, "BRL"));
    }

    [Fact]
    public void Position_WhenBuyAdded_ShouldUpdateQuantityAndRecalculateAveragePrice()
    {
        // Compra 1: 100 acoes a R$30 = R$3000
        // Compra 2: 50 acoes a R$40  = R$2000
        // Total: 150 acoes, preco medio = R$5000/150 = R$33,33
        var position = new Position(CreateAsset(), new Quantity(100m), new Money(30m, "BRL"));
        var buyTransaction = new Transaction(CreateAsset(), new Quantity(50m), new Money(40m, "BRL"), DateTimeOffset.UtcNow, TransactionType.Buy);

        position.Apply(buyTransaction);

        position.Quantity.Should().Be(new Quantity(150m));
        position.AveragePrice.Amount.Should().BeApproximately(33.33m, 0.01m);
    }

    [Fact]
    public void Position_WhenSellAdded_ShouldReduceQuantityAndKeepAveragePrice()
    {
        var position = new Position(CreateAsset(), new Quantity(100m), new Money(30m, "BRL"));
        var sellTransaction = new Transaction(CreateAsset(), new Quantity(40m), new Money(35m, "BRL"), DateTimeOffset.UtcNow, TransactionType.Sell);

        position.Apply(sellTransaction);

        position.Quantity.Should().Be(new Quantity(60m));
        position.AveragePrice.Should().Be(new Money(30m, "BRL"));
    }

    [Fact]
    public void Position_WhenSellExceedsQuantity_ShouldThrowException()
    {
        var position = new Position(CreateAsset(), new Quantity(10m), new Money(30m, "BRL"));
        var sellTransaction = new Transaction(CreateAsset(), new Quantity(50m), new Money(35m, "BRL"), DateTimeOffset.UtcNow, TransactionType.Sell);

        var act = () => position.Apply(sellTransaction);

        act.Should().Throw<DomainException>()
            .WithMessage("*quantity*");
    }
}
