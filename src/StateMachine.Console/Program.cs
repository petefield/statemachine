using ExampleProject;
// var m = new ManifestGenerator().GetManifest();
// var json = JsonSerializer.Serialize(m, new JsonSerializerOptions { WriteIndented = true });
// Console.WriteLine(json);
// Console.ReadKey();

var eventSteam = new EventStream();
var viewService = new ViewService(eventSteam);

var customerId  = Guid.NewGuid();
Console.WriteLine(viewService.Get(customerId));

foreach (var evt in EventSteam())
{
    eventSteam.AddEvent(customerId, evt);
    Console.WriteLine(viewService.Get(customerId));
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
  yield return new DeathReported();
  yield return new DeathConfirmed();
    yield return new AccountOpened();

}