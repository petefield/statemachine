namespace CustomerStateManagement.Domain.Events;

internal record InvestigationCompleted(bool Outcome) : DomainEvent
{
}


