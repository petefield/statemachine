namespace ExampleProject
{
    internal class ViewService
    {
        private readonly EventStream eventStream;

        public ViewService(EventStream eventStream)
        {
            this.eventStream = eventStream;
        }

        public CustomerView Get(Guid CustomerId)
        { 
            var events = eventStream.GetAllEvents(CustomerId);
            return new CustomerView(events);
        }
    }
}
