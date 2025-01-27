namespace CustomerStateManagement.EventSourcing
{
    public abstract class StreamView
    {
        public bool ApplyAll(IEnumerable<DomainEvent> domainEvents)
        {
            bool allEventsApplied = true; 

            foreach (var domainEvent in domainEvents)
            {
                allEventsApplied = Apply(domainEvent);
                if (!allEventsApplied) 
                    break;
            }

            return allEventsApplied;
        }
        public abstract bool Apply(DomainEvent domainEvent);
    }
}
