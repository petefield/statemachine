namespace CustomerStateManagement.EventSourcing
{
    internal interface IStreamView
    {
        public abstract bool Apply(IDomainEvent domainEvents);
    }
}
