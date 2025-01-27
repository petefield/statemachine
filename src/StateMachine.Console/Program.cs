
var eventSteam = new EventStream();
var viewService = new ViewService(eventSteam);

Console.WriteLine($"{Column("Received Event", 45)}|{Column( "State", 20 )}|{Column("Accounts", 15) }|");
Console.WriteLine(new string('-',83));

var customerId = Guid.NewGuid();

foreach (var evt in EventSteam())
{
    eventSteam.AddEvent(customerId, evt);

    var customer = viewService.Get<CustomerView>(customerId);

    DisplayCustomer(evt, customer);
}

static IEnumerable<DomainEvent> EventSteam()
{
    yield return new CustomerCreated();
    yield return new DetailsProvided();
    yield return new RiskCheckPassed();
    yield return new AccountOpened();
    yield return new AccountClosed();
    yield return new AccountOpened();
    yield return new AccountOpened();
    yield return new AccountOpened();
    yield return new AccountClosed();
    yield return new InvestigationStarted();
    yield return new InvestigationCompleted(Outcome: true);
    yield return new InvestigationStarted();
    yield return new InvestigationCompleted(Outcome: false);
    yield return new AccountOpened();

}

void DisplayCustomer(DomainEvent domainEvent, CustomerView customer) {

    var eventdescription = domainEvent.ToString().Replace("{ }", "").Trim();

    Console.Write($"{Column(eventdescription, 45)}| ");
    Console.Write($"{Column(customer.State.ToString() ?? "null", 19)}| ");
    Console.WriteLine($"{Column(customer.AccountsHeld, 14)}|");
}

string Column(object value, int width) => value.ToString()!.PadRight(width).Substring(0, width);
