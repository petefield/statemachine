public interface ITransition<TSubject, TState> where TState :  struct,Enum
{
    TState End { get; }

    System.Nullable<TState> Start { get; }

    bool ForTransitionType(IEvent @event);

    bool MatchCondition(TSubject sub, IEvent @event);


    public bool hasCondition() ;

}