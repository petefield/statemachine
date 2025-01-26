using CustomerStateManagement.EventSourcing;

namespace EventSourcing;

internal class ViewService
{
    private readonly EventStream eventStream;

    public ViewService(EventStream eventStream)
    {
        this.eventStream = eventStream;
    }

    public T Get<T>(Guid CustomerId) where T : IStreamView, new()
    {
        var events = eventStream.GetAllEvents(CustomerId);
        var view = new T();
        foreach (var e in events)
        {
            view.Apply(e);
        }
        return view;
    }
}
