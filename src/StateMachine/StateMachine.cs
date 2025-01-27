using StateMachine.Exceptions;

namespace StateMachine;

public class StateMachine<TSubject, TState> where TState: struct, Enum where TSubject : IStateMachine<TState>
{
    public List<ITransition<TSubject, TState>> Transitions = [];

    public void AddTransition<TEvent>(TState? From, TState To ) => AddTransition<TEvent>(From, To, null);
    
    public void AddTransition<TEvent>(TState? From, TState To, Func<TSubject, TEvent, bool>? condition)
    {
        var t = new Transition<TEvent, TSubject, TState>(From, To, condition, null);
        this.Transitions.Add(t);
    }

    public void UpdateState(TSubject subject, IEvent evt)
    {
        subject.State = GetNextState(subject, subject.State, evt);
    }

    public TState GetNextState(TSubject subject, TState? currentState, IEvent evt)
    {
        var candidateTransitions = Transitions.Where(
            t => t.ForTransitionType(evt)
            && t.Start.Equals(currentState)
            && t.MatchCondition(subject, evt)).ToList();

        if (!candidateTransitions.Any())
            throw new Exception($"Invalid state transition from `{currentState}` for event ` {evt.GetType().Name}`");

        if (candidateTransitions.Count() > 1)
        {
            var TransitionWithCondition = candidateTransitions.SingleOrDefault(c => c.hasCondition());

            if (TransitionWithCondition is not null)
                return TransitionWithCondition.End;

            var TransitionWithoutCondition = candidateTransitions.SingleOrDefault(c => !c.hasCondition());

            if (TransitionWithoutCondition is not null)
                return TransitionWithoutCondition.End;


            throw new MultipleStateMatchesException($"Multiple possible state transitions found while attempting to apply '{evt.GetType().Name}' to a subject with state `{subject.State}`. ", candidateTransitions.Select(transition => transition.ToString()).ToArray());


        }

        return candidateTransitions.Single().End;
    }

}