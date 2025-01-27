namespace EventSourcing;

public abstract record DomainEvent : IEvent
{
    public override string ToString()
    {
        return GetType().Name;
    }
}
