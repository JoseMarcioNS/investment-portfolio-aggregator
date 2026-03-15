using InvestmentPortfolio.Domain.Exceptions;

namespace InvestmentPortfolio.Domain.Entities;

public class BrokerAccount : Entity
{
    public string Name { get; private set; }
    public Guid UserId { get; private set; }

    public BrokerAccount(string name, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("The name is required.");

        if (userId == Guid.Empty)
            throw new DomainException("The userId cannot be empty.");

        Name = name;
        UserId = userId;
    }
}
