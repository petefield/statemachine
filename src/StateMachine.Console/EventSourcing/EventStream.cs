namespace EventSourcing;

internal class EventStream
{
    private readonly Dictionary<Guid, List<IDomainEvent>> _events = [];

    public void AddEvent(Guid id, IDomainEvent evt)
    {
        Console.WriteLine($"\tAdding {evt.GetType().Name} to EventStream for customer {id}");

        if (_events.ContainsKey(id))
        {
            _events[id].Add(evt);
        }
        else
        {
            _events.Add(id, [evt]);
        }
    }

    public IEnumerable<IDomainEvent> GetAllEvents(Guid id) => _events.TryGetValue(id, out var events)
        ? events
        : Enumerable.Empty<IDomainEvent>();
}