using StateMachine.Generator;

namespace ExampleProject.Models;

[GenerateStateMap<CustomerView, CustomerState>("Deceased-Day-2.md")]
public partial class CustomerView : ISubject<CustomerState>
{
    public CustomerView(IEnumerable<IDomainEvent> events)
    {
        foreach (var evt in events)
            HandleDomainEvent(evt);
    }

    public CustomerState State { get; set; }


    public int AccountsHeld { get; set; }

    public bool HandleDomainEvent(IDomainEvent evt)
    {
        try {
            Console.Write($"\tUpdating Customer View with {evt.GetType().Name}...");

            var result = evt switch
            {
                AccountOpened e => HandleAccountOpened(e),
                AccountClosed e => HandleAccountClosed(e),
                _ => true
            };

            if (result)
                ApplyTransition(this, evt);

            Console.WriteLine($"{result} : State is now {State}");

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    private bool HandleAccountOpened(AccountOpened e) {
        AccountsHeld++;
        return true;
    }
    
    private bool HandleAccountClosed(AccountClosed e)
    {
        AccountsHeld--;
        return true;
    }

    public override string ToString()
    {
        return $"Customer {{ Accounts: {AccountsHeld}, State: {State} }}";
    }
}