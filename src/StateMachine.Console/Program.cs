
using CustomerStateManagement.Domain.Customer;

var eventSteam = new EventStream();
var viewService = new ViewService(eventSteam);

var customerId = Guid.NewGuid();
Console.WriteLine(viewService.Get<CustomerView>(customerId));

foreach (var evt in EventSteam())
{
    eventSteam.AddEvent(customerId, evt);
    Console.WriteLine(viewService.Get<CustomerView>(customerId));
}

static IEnumerable<IDomainEvent> EventSteam()
{
    yield return new DetailsProvided();
    yield return new RiskCheckPassed();
    yield return new AccountOpened();
    yield return new AccountClosed();
    yield return new AccountOpened();
    yield return new AccountOpened();
    yield return new AccountClosed();
    yield return new AccountClosed();
    yield return new AccountOpened();
    yield return new AccountOpened();
    yield return new InvestigationStarted();
    yield return new InvestigationCompleted(Outcome: false);
}