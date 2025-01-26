using System.Reflection;

namespace StateMachine.ManifestGenerator;

internal class ManifestGenerator
{
    bool ImplementsIStateManaged(Type t) => t.GetInterfaces()
       .Any(i => i.IsGenericType &&
                 i.GetGenericTypeDefinition() == typeof(IStateMachine<>));

    StateChangeEvent[] GetEvents() => Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(x => !x.IsInterface && typeof(IStateChangeEvent).IsAssignableFrom(x))
        .Select(x => new StateChangeEvent(
            Name: x.Name,
            Properties: x.GetProperties().Select(p => new Property(p.Name, new TypeInfo(p.PropertyType.FullName ?? "", GetEnumValueNames(p.PropertyType)))).ToArray()))
        .ToArray();

    Entity[] GetEntities() => Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(ImplementsIStateManaged)
        .Select(et =>
        {
            var statePropertyType = et.GetProperty(nameof(IStateMachine<object>.State))?.PropertyType;

            if (statePropertyType is null)
                throw new ArgumentException("");

            return new Entity(et.Name)
            {
                StateTypeName = statePropertyType.FullName ?? string.Empty,
                States = GetEnumValueNames(statePropertyType)?.Select(x => x ?? "")?.ToList() ?? [""]
            };
        }).ToArray();

    public Manifest GetManifest()
    {
        return new Manifest("ExampleProject")
        {
            Events = GetEvents(),
            Entities = GetEntities()
        };
    }

    private string?[]? GetEnumValueNames(Type enumType)
    {
        return enumType.IsEnum
             ? Enum.GetValues(enumType).Cast<object>().Select(x => x.ToString()).ToArray()
             : null;

    }
}
