namespace CustomerStateManagement.Events;

internal record InvestigationCompleted(bool Outcome) : IDomainEvent
{
}


