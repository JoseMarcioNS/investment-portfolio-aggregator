using FluentAssertions;
using InvestmentPortfolio.Domain.Entities;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.Domain.Tests.Entities;

public class PortfolioTests
{
    private static Asset CreateAsset(string ticker = "PETR4") =>
        new(new Ticker(ticker), "Company", AssetType.Stock);

    private static BrokerAccount CreateBrokerAccount() =>
        new("XP Investimentos", Guid.NewGuid());

    [Fact]
    public void Portfolio_WhenCreated_ShouldHaveEmptyPositions()
    {
        var portfolio = new Portfolio(CreateBrokerAccount());

        portfolio.Id.Should().NotBe(Guid.Empty);
        portfolio.Positions.Should().BeEmpty();
    }

    [Fact]
    public void Portfolio_WhenCreatedWithNullBrokerAccount_ShouldThrowException()
    {
        var act = () => new Portfolio(null!);

        act.Should().Throw<DomainException>()
            .WithMessage("*brokerAccount*");
    }

    [Fact]
    public void Portfolio_WhenPositionAdded_ShouldContainIt()
    {
        var portfolio = new Portfolio(CreateBrokerAccount());
        var position = new Position(CreateAsset(), new Quantity(100m), new Money(30m, "BRL"));

        portfolio.AddPosition(position);

        portfolio.Positions.Should().ContainSingle();
    }

    [Fact]
    public void Portfolio_WhenSameAssetPositionAddedTwice_ShouldThrowException()
    {
        var portfolio = new Portfolio(CreateBrokerAccount());
        var asset = CreateAsset();
        portfolio.AddPosition(new Position(asset, new Quantity(100m), new Money(30m, "BRL")));

        var act = () => portfolio.AddPosition(new Position(asset, new Quantity(50m), new Money(35m, "BRL")));

        act.Should().Throw<DomainException>()
            .WithMessage("*already exists*");
    }

    [Fact]
    public void Portfolio_TotalValue_ShouldBeSumOfAllPositions()
    {
        var portfolio = new Portfolio(CreateBrokerAccount());
        portfolio.AddPosition(new Position(CreateAsset("PETR4"), new Quantity(100m), new Money(30m, "BRL")));
        portfolio.AddPosition(new Position(CreateAsset("VALE3"), new Quantity(50m), new Money(80m, "BRL")));

        // PETR4: 100 * 30 = 3000 + VALE3: 50 * 80 = 4000 = 7000
        portfolio.TotalValue.Should().Be(new Money(7000m, "BRL"));
    }

    [Fact]
    public void Portfolio_GetPositionByTicker_WhenExists_ShouldReturnPosition()
    {
        var portfolio = new Portfolio(CreateBrokerAccount());
        var asset = CreateAsset("PETR4");
        portfolio.AddPosition(new Position(asset, new Quantity(100m), new Money(30m, "BRL")));

        var position = portfolio.GetPositionByTicker(new Ticker("PETR4"));

        position.Should().NotBeNull();
        position!.Asset.Ticker.Symbol.Should().Be("PETR4");
    }

    [Fact]
    public void Portfolio_GetPositionByTicker_WhenNotExists_ShouldReturnNull()
    {
        var portfolio = new Portfolio(CreateBrokerAccount());

        var position = portfolio.GetPositionByTicker(new Ticker("AAPL"));

        position.Should().BeNull();
    }
}
