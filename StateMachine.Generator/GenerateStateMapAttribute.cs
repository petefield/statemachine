using System;

namespace StateMachine.Generator;

#pragma warning disable CS9113 // Parameter is unread.
public class GenerateStateMapAttribute<TSubject, TState>(string FilePath) : Attribute
#pragma warning restore CS9113 // Parameter is unread.
{
}
