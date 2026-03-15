using FluentAssertions;
using InvestmentPortfolio.Domain.Entities;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.Domain.Tests.Entities;

public class AssetTests
{
    [Fact]
    public void Asset_WhenCreatedWithValidData_ShouldHaveCorrectProperties()
    {
        var ticker = new Ticker("PETR4");
        var asset = new Asset(ticker, "Petrobras", AssetType.Stock);

        asset.Id.Should().NotBe(Guid.Empty);
        asset.Ticker.Should().Be(ticker);
        asset.Name.Should().Be("Petrobras");
        asset.Type.Should().Be(AssetType.Stock);
    }

    [Fact]
    public void Asset_WhenCreatedWithNullTicker_ShouldThrowException()
    {
        var act = () => new Asset(null!, "Petrobras", AssetType.Stock);

        act.Should().Throw<DomainException>()
            .WithMessage("*ticker*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Asset_WhenCreatedWithInvalidName_ShouldThrowException(string? name)
    {
        var act = () => new Asset(new Ticker("PETR4"), name!, AssetType.Stock);

        act.Should().Throw<DomainException>()
            .WithMessage("*name*");
    }
}
