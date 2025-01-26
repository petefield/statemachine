namespace ExampleProject;

public record Manifest(string Name)
{
    public StateChangeEvent[] Events { get; set; } = Array.Empty<StateChangeEvent>();
    public Entity[] Entities { get; set; } = Array.Empty<Entity>();
}
public record Entity(string Name)
{
    public string StateTypeName { get; set; } = string.Empty;
    public IList<string> States { get; set; } = [];
}


public record TypeInfo(string Name, string?[]? Values);

public record Property(string Name, TypeInfo Type);

public record StateChangeEvent(string Name, Property[] Properties);
