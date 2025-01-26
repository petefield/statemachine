﻿using StateMachine.SourceGenerator;

namespace CustomerStateManagement.Domain.Customer;

[GenerateStateMap("Deceased-Day-2.md")]
public partial class CustomerView : IStateMachine<CustomerState>, IStreamView
{
    public CustomerState State { get; set; }

    public int AccountsHeld { get; set; }

    public bool Apply(DomainEvent evt)
    {
        try
        {
            var result = evt switch
            {
                AccountOpened e => HandleAccountOpened(e),
                AccountClosed e => HandleAccountClosed(e),
                _ => true
            };

            if (result)
                ApplyTransition(this, evt);

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
}