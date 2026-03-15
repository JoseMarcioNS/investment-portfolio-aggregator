using FluentAssertions;
using InvestmentPortfolio.Domain.Entities;
using InvestmentPortfolio.Domain.Exceptions;

namespace InvestmentPortfolio.Domain.Tests.Entities;

public class BrokerAccountTests
{
    [Fact]
    public void BrokerAccount_WhenCreatedWithValidData_ShouldHaveCorrectProperties()
    {
        var userId = Guid.NewGuid();
        var account = new BrokerAccount("XP Investimentos", userId);

        account.Id.Should().NotBe(Guid.Empty);
        account.Name.Should().Be("XP Investimentos");
        account.UserId.Should().Be(userId);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void BrokerAccount_WhenCreatedWithInvalidName_ShouldThrowException(string? name)
    {
        var act = () => new BrokerAccount(name!, Guid.NewGuid());

        act.Should().Throw<DomainException>()
            .WithMessage("*name*");
    }

    [Fact]
    public void BrokerAccount_WhenCreatedWithEmptyUserId_ShouldThrowException()
    {
        var act = () => new BrokerAccount("XP Investimentos", Guid.Empty);

        act.Should().Throw<DomainException>()
            .WithMessage("*userId*");
    }
}
