public interface IStateMachine<TState> where TState:  struct, Enum
{
    System.Nullable<TState> State { get; set; }
}