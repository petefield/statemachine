using StateMachine.SourceGenerator;

namespace CustomerStateManagement.Domain.Customer;

[GenerateStateMap("CustomerViewStateDiagram.md")]
public partial class CustomerView : StreamView, IStateMachine<CustomerState>
{
    public CustomerState? State { get; set; }

    public int AccountsHeld { get; set; }

    public override bool Apply(DomainEvent evt)
    {
        try
        {

            UpdateState(evt);

            var result = evt switch
            {
                AccountOpened e => HandleAccountOpened(e),
                AccountClosed e => HandleAccountClosed(e),
                _ => true
            };


            return result;
        }
        catch (Exception)
        {
            var fg = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Event `{evt.GetType().Name}` cannot be applied to a customer with state '{State}'");
            Console.ForegroundColor = fg;

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
}