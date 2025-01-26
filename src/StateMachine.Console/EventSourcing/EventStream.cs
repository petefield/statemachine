namespace EventSourcing;

internal class EventStream
{
    private readonly Dictionary<Guid, List<DomainEvent>> _events = [];

    public void AddEvent(Guid id, DomainEvent evt)
    {

        if (_events.ContainsKey(id))
        {
            _events[id].Add(evt);
        }
        else
        {
            _events.Add(id, [evt]);
        }
    }

    public IEnumerable<DomainEvent> GetAllEvents(Guid id) => _events.TryGetValue(id, out var events)
        ? events
        : Enumerable.Empty<DomainEvent>();
}