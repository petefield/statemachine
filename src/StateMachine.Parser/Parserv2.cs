using StateDiagram.Parser;

namespace StateDiagram.Parser;

public static class Parserv2
{
    public static IEnumerable<TransitionDetails> Parse(string input)
    {
        var transitions = new List<TransitionDetails>();
        var choices = new List<string>();

        var lines = input.Split('\n')
            .Skip(2)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Where(line => !line.Trim().StartsWith("[*]"))
            .Where(line => !line.Trim().EndsWith("[*]"))
            .Select(x => x.Trim().Replace("\\", string.Empty));

        // Parse the input and return the result
        foreach (var line in lines)
        {
            if (line.StartsWith("```"))
                break;

            if (line.StartsWith("state") && line.EndsWith("<<choice>>"))
            {
                choices.Add(line.Split([' '], StringSplitOptions.RemoveEmptyEntries)[1]);
                continue;
            }

            if (!string.IsNullOrEmpty(line))
            {
                var Linetokens = line.Split(["-->"], StringSplitOptions.None);

                var tokens = ParseLine(Linetokens);

                transitions.Add( new TransitionDetails
                {
                    From = tokens.startState,
                    Event = tokens.trigger,
                    To = tokens.endState,
                });
            }
        }

        foreach (var transition in transitions)
        {
            if (choices.Contains(transition.To)) // This isn't a real transition, it's a pointer to a choice.
            {
                foreach (var choice in transitions.Where(t => t.From == transition.To))
                {
                    yield return new TransitionDetails
                    {
                        From = transition.From,
                        Event = transition.Event,
                        To = choice.To,
                        Condition = choice.Event.Replace("if ", "")
                    };
                }
                continue;
            }

            if (choices.Contains(transition.From)) //this isnt a real transition, it's a choice.
                continue;

            yield return transition;
        }
    }

    private static (string startState, string endState, string trigger) ParseLine(string[] tokens)
    {
        var t = tokens[1].Split(':');
        var startState = tokens[0].Trim();
        var endState = t[0].Trim();
        var description = t[1].Trim().Split('[');
        var trigger = description[0].Trim();

        return (startState, endState, trigger);

    }



}
