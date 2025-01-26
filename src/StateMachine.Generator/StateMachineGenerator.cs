using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using pfie.http.sourcegen;
using System.Linq;
using System.IO;
using System;
using StateMachine.SourceGenerator;
using Microsoft.CodeAnalysis.Operations;

namespace StateMachine.Generator;

[Generator]
public partial class StateMachineGenerator : IIncrementalGenerator
{
    private static StateMachineDescription GetServiceDetail(GeneratorAttributeSyntaxContext ctx)
    {
        try
        {
            var attribute = ctx.Attributes.First();

            var path = attribute.ConstructorArguments[0].Value?.ToString();

            if (string.IsNullOrWhiteSpace(path))
                throw new Exception("Path is required");

            var classDeclaration = (ClassDeclarationSyntax)ctx.TargetNode;
            var symbol = ctx.SemanticModel.GetDeclaredSymbol(classDeclaration) as ITypeSymbol;

            var interfaces = symbol!.AllInterfaces;

            var statemachineInterface = interfaces.SingleOrDefault(x => x.Name == "IStateMachine")
                ?? throw new Exception("ISubject interface not found");

            var stateTypeName = statemachineInterface.TypeArguments.First().Name;

            var classname = classDeclaration.Identifier.Text;

            var namespaceNode = classDeclaration
                .Ancestors()
                .Where(x => x is FileScopedNamespaceDeclarationSyntax)
                .First();

            var namespaceName = ((FileScopedNamespaceDeclarationSyntax)namespaceNode).Name.ToString();

            return new StateMachineDescription(path!, classname, stateTypeName, namespaceName);
        }
        catch (Exception e)
        {
            throw new Exception($"{nameof(GetServiceDetail)} : {e.Message}");
        }
    }

    private class ConfigurationFile
    {
        public ConfigurationFile(string path, string code)
        {
            Path = path;
            Code = code;
        }

        public string Path { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        try
        {

            var additionalFiles = context.AdditionalTextsProvider
            .Select(static (text, cancellationToken) =>
            {
                var name = Path.GetFileName(text.Path);
                var code = text.GetText(cancellationToken)?.ToString() ?? string.Empty;
                return new ConfigurationFile(name, code);
            }).Collect();

            var stateMachines = context.SyntaxProvider.ForAttributeWithMetadataName(typeof(GenerateStateMapAttribute).FullName,
                predicate: static (node, _) => true,
                transform: static (ctx, _) => GetServiceDetail(ctx)).Collect();

            var combined = IncrementalValueProviderExtensions.Combine(additionalFiles, stateMachines);

            context.RegisterSourceOutput(combined, Execute);
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(Initialize)} : {ex.Message} - {ex.StackTrace}", ex);
        }
    }

    private void Execute(SourceProductionContext context, (ImmutableArray<ConfigurationFile> configurationFiles, ImmutableArray<StateMachineDescription> stateMachines) tuple)
    {
        foreach (var stateMachine in tuple.stateMachines)
        {
            var configFile = tuple.configurationFiles.SingleOrDefault(x => x.Path == stateMachine.Path);

            if (configFile == null)
                throw new Exception($"State Machine configuration `{stateMachine.Path}` could not be found ");

            var source = CSharpClientBuilder.Build(stateMachine, configFile.Code);

            context.AddSource($"StateMachines/{stateMachine.SubjectType}.g.cs", source);
        }
    }
}