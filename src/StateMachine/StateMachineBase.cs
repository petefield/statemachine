using StateMachine.Exceptions;

public abstract class StateMachineBase<TSubject, TState>
    where TState : Enum
    where TSubject : IStateMachine<TState>
{
    protected abstract List<ITransition<TSubject, TState>> Transitions { get; }

    public void ApplyTransition(TSubject subject, IStateChangeEvent evt)
    {
        subject.State = GetNextState(subject.State, evt, subject);
    }

    public TState GetNextState(TState currentState, IStateChangeEvent evt, TSubject subject)
    {
        var candidateTransitions = Transitions.Where(
            t => t.ForTransitionType(evt)
            && t.Start.Equals(currentState)
            && t.MatchCondition(subject, evt)).ToList();

        if (!candidateTransitions.Any())
            throw new Exception($"Invalid state transition from {currentState} for event {evt.GetType().Name}");

        if (candidateTransitions.Count() > 1)
            throw new MultipleStateMatchesException($"Multiple possible state transitions found while attempting to apply '{evt.GetType().Name}' to a subject with state `{subject.State}`. ", candidateTransitions.Select(transition => transition.ToString()).ToArray());

        return candidateTransitions.Single().End;
    }
}
