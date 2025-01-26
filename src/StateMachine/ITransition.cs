public interface ITransition<TSubject, TState>
{
    TState End { get; }

    TState Start { get; }

    bool ForTransitionType(IStateChangeEvent @event);

    bool MatchCondition(TSubject sub, IStateChangeEvent @event);

}