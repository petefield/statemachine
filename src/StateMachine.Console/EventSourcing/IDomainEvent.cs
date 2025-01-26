namespace EventSourcing;

public abstract record DomainEvent : IStateChangeEvent
{
    public override string ToString()
    {
        return GetType().Name;
    }
}
