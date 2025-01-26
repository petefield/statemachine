public interface ISubject<TState>
{
    TState State { get; set; }
}