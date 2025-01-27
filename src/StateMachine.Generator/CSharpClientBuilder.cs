﻿using System.Text;
using StateDiagram.Parser;

namespace StateMachine.SourceGenerator
{
    public class CSharpClientBuilder
    {
        public static string Build(StateMachineDescription service, string config)
        {
            var sb = new StringBuilder();
            var transitions = Parserv2.Parse(config);

            sb.AppendLine($"//Autogenerated 27");

            sb.AppendLine($"namespace {service.NamespaceName};");

            sb.AppendLine($"public partial class {service.SubjectType} : StateMachineBase<{service.SubjectType}, {service.StateType}> ");
            sb.AppendLine($"{{");

            sb.AppendLine($"\tprotected override List<ITransition<{service.SubjectType}, {service.StateType}>> Transitions => [");

            foreach (var transition in transitions)
            {
                var cond = string.IsNullOrWhiteSpace(transition.Condition)
                    ? "null"
                    : $"(subject, evt) =>  {transition.Condition}";

                var d = $"{transition.Condition.Replace($"\"", "`")}";

                sb.AppendLine($"\t\t new Transition<{transition.Event}, {service.SubjectType}, {service.StateType}>({service.StateType}.{transition.From}, {service.StateType}.{transition.To}, {cond}, \"{d}\"),");

            }

            sb.AppendLine("\t];");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }

}
