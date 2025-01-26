public interface IStateMachine<TState>
{
    TState State { get; set; }
}