using CustomerStateManagement.EventSourcing;

namespace EventSourcing;

internal class ViewService
{
    private readonly EventStream eventStream;

    public ViewService(EventStream eventStream)
    {
        this.eventStream = eventStream;
    }

    public T Get<T>(Guid CustomerId) where T : StreamView, new()
    {
        var events = eventStream.GetAllEvents(CustomerId);
        var view = new T();
        view.ApplyAll(events);
        return view;
    }
}
