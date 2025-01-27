namespace StateMachine.SourceGenerator
{
    public class StateMachineDescription
    {
        public StateMachineDescription(string path, string subjectType, string stateType, string namespaceName)
        {
            Path = path;
            SubjectType = subjectType;
            StateType = stateType;
            NamespaceName = namespaceName;
        }


        public string Path { get; }
        public string SubjectType { get; }
        public string StateType { get; }
        public string NamespaceName { get; }
    }
}
