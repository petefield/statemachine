public class Transition<TTrigger, TSubject, TState>(
    TState? start, 
    TState end, 
    Func<TSubject, TTrigger, bool>? condition, string? conditionDescription) : ITransition<TSubject, TState>
    where TSubject : IStateMachine<TState>
    where TState: struct,Enum
{
    public TState? Start { get; } = start;

    public TState End { get; } = end;

    public Func<TSubject, TTrigger, bool>? Condition { get; } = condition;

    public bool ForTransitionType(IEvent @event) => typeof(TTrigger) == @event.GetType();

    public bool MatchCondition(TSubject sub, IEvent @event) => Condition?.Invoke(sub, (TTrigger)@event) ?? true;

    public string ConditionDescription { get; } = conditionDescription ?? string.Empty;

    public override string ToString()
    {
        string when = string.IsNullOrWhiteSpace(ConditionDescription)
            ? string.Empty
            : $" with condition '{ConditionDescription}' ";

        return $"'{Start}' -> {typeof(TTrigger).Name} -> '{End}' {when}";
    }
}
