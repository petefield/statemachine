

namespace pfie.http.sourcegen
{
    public class StateMachineDescription
    {
        public StateMachineDescription(string path, string subjectType, string stateType, string className,  string namespaceName)
        {
            Path = path;
            SubjectType = subjectType;
            StateType = stateType;
            Classname = className;
            NamespaceName = namespaceName;
        }


        public string Path { get; }
        public string SubjectType { get; }
        public string StateType { get; }
        public string Classname { get; }
        public string NamespaceName { get; }
    }
}
