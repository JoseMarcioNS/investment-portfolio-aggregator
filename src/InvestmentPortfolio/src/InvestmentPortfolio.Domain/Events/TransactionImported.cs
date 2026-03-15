namespace InvestmentPortfolio.Domain.Events;

public class TransactionImported : DomainEvent
{
    public Guid TransactionId { get; }

    public TransactionImported(Guid transactionId) : base(transactionId)
    {
        TransactionId = transactionId;
    }
}