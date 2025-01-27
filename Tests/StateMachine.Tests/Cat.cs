

namespace StateMachine.Tests;

public record CatFed : IEvent;
public record CatTeased : IEvent;
public record CatNotFed(string reason) : IEvent;

public enum CatState
{
    Happy,
    Angry,
    Hungry,
    Dead
}

public class Cat() : IStateMachine<CatState>
{
    public int Hunger = 0;

    public StateMachine<Cat, CatState> StateMachine { get; set; } = new StateMachine<Cat, CatState>();

    public CatState? State { get; set; }

    public void ApplyEvent(IEvent evt)
    {
        StateMachine.UpdateState(this, evt);
    }

}
