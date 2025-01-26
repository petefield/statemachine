public class Transition<TTrigger, TSubject, TState> : ITransition<TSubject, TState> where TSubject : ISubject<TState>  
{
    public Transition(TState start, TState end, Func<TSubject, TTrigger, bool>? condition, string? conditionDescription )
    {
        Start = start;
        End = end;
        Condition = condition;
        ConditionDescription = conditionDescription ?? string.Empty;
    }

    public TState Start { get; }

    public TState End { get; }

    public Func<TSubject, TTrigger, bool>? Condition { get; }

    public bool ForTransitionType(IStateChangeEvent @event) => typeof(TTrigger) == @event.GetType();

    public bool MatchCondition(TSubject sub, IStateChangeEvent @event) => Condition?.Invoke(sub, (TTrigger)@event) ?? true;

    public string ConditionDescription { get; }

    public override string ToString()
    {
        string when = string.IsNullOrWhiteSpace( ConditionDescription )
            ? string.Empty
            : $" with condition '{ConditionDescription}' ";

        return $"'{Start}' -> {typeof(TTrigger).Name} -> '{End}' {when}" ;
    }
}
