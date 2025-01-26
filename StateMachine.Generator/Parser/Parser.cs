//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Headers;

//namespace StateMachine.Generator.Parser
//{
//    public static class Parser
//    {
//        public static IEnumerable<TransitionData> Parse(string input)
//        {

//            var lines = input.Split('\n')
//                .Skip(2)
//                .Where(line => !string.IsNullOrWhiteSpace(line))
//                .Where(line => !line.Trim().StartsWith("[*]"))
//                .Select(x => x.Trim().Replace("\\", string.Empty));

//            // Parse the input and return the result
//            foreach (var line in lines)
//            {
//                if (line == "```")
//                    break;

//                if (!string.IsNullOrEmpty(line))
//                {
//                    var Linetokens = line.Split(["-->"], StringSplitOptions.None);

//                    var tokens = ParseLine(Linetokens);

//                    yield return new TransitionData
//                    {
//                        StartingState = tokens.startState,
//                        Trigger = tokens.trigger,
//                        EndingState = tokens.endState,
//                        Condition = tokens.condition
//                    };
//                }
//            }
//        }

//        private static (string startState, string endState, string trigger, string condition) ParseLine(string[] tokens)
//        {
//            var t = tokens[1].Split(':');
//            var startState = tokens[0].Trim();
//            var endState = t[0].Trim();
//            var description = t[1].Trim().Split('[');
//            var trigger = description[0].Trim();
//            var condition = description.Length > 1 ? description[1].Trim().TrimEnd(']') : string.Empty;

//            return (startState, endState, trigger, condition);
//        }
//    }
//}
