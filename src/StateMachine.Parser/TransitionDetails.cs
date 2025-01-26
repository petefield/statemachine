namespace StateDiagram.Parser
{
    public class TransitionDetails
    {
        public string From { get; set; } = string.Empty;
        public string Event { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"From {From} to {To} when {Event}{(string.IsNullOrWhiteSpace(Condition) ? "" : " if ")}{Condition}";
        }
    }
}
