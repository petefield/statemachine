
using CustomerStateManagement.EventSourcing;
using StateMachine.SourceGenerator;

namespace CustomerStateManagement.Domain.Customer;

[GenerateStateMap("Deceased-Day-2.md")]
public partial class CustomerView : IStateMachine<CustomerState>, IStreamView
{
    public CustomerState State { get; set; }

    public int AccountsHeld { get; set; }

    public bool Apply(IDomainEvent evt)
    {
        try
        {
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

    private bool HandleAccountOpened(AccountOpened e)
    {
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

    public bool Apply(IEnumerable<IDomainEvent> domainEvents)
    {
        throw new NotImplementedException();
    }
}