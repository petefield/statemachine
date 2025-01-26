namespace StateMachine.Exceptions;

public class MultipleStateMatchesException : Exception
{
    public MultipleStateMatchesException(string message, string[] matches) : base(message)
    {
        Matches = matches;
    }

    public string[] Matches { get; }
}
