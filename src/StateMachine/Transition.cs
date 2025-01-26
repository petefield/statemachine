public class Transition<TTrigger, TSubject, TState>(
    TState start, 
    TState end, 
    Func<TSubject, TTrigger, bool>? condition, string? conditionDescription) : ITransition<TSubject, TState>
    where TSubject : IStateMachine<TState>
{
    public TState Start { get; } = start;

    public TState End { get; } = end;

    public Func<TSubject, TTrigger, bool>? Condition { get; } = condition;

    public bool ForTransitionType(IStateChangeEvent @event) => typeof(TTrigger) == @event.GetType();

    public bool MatchCondition(TSubject sub, IStateChangeEvent @event) => Condition?.Invoke(sub, (TTrigger)@event) ?? true;

    public string ConditionDescription { get; } = conditionDescription ?? string.Empty;

    public override string ToString()
    {
        string when = string.IsNullOrWhiteSpace(ConditionDescription)
            ? string.Empty
            : $" with condition '{ConditionDescription}' ";

        return $"'{Start}' -> {typeof(TTrigger).Name} -> '{End}' {when}";
    }
}
