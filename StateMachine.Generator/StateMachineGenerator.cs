using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using pfie.http.sourcegen;
using System.Linq;
using System.IO;
using System;

namespace StateMachine.Generator;

[Generator]
public partial class StateMachineGenerator : IIncrementalGenerator
{

    private static StateMachineDescription GetServiceDetail(GeneratorAttributeSyntaxContext ctx)
    {   
        try{
            var attribute = ctx.Attributes.First();

            var path = attribute.ConstructorArguments[0].Value?.ToString();
            
            if (path is null)
                throw new Exception("path not found");

            var classDeclartion = (ClassDeclarationSyntax)ctx.TargetNode;

            var b = (GenericNameSyntax) classDeclartion.AttributeLists[0].Attributes[0].Name;
            var subjectType = b.TypeArgumentList.Arguments[0].ToString();
            var stateType = b.TypeArgumentList.Arguments[1].ToString();

            var classname = classDeclartion.Identifier.Text;

            var namespaceNode = classDeclartion
                .Ancestors()
                .Where(x => x is FileScopedNamespaceDeclarationSyntax)
                .First();

            var namespaceName = ((FileScopedNamespaceDeclarationSyntax)namespaceNode).Name.ToString();

            return new StateMachineDescription(path, subjectType, stateType, classname, namespaceName);
        }
        catch(Exception e)
        {
            throw new Exception($"{nameof(GetServiceDetail)} : {e.Message}")       ;
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
        try {

            var additionalFiles = context.AdditionalTextsProvider
            .Select(static (text, cancellationToken) =>
            {
                var name = Path.GetFileName(text.Path);
                var code = text.GetText(cancellationToken)?.ToString() ?? string.Empty;
                return new ConfigurationFile (name, code);
            }).Collect();

            var stateMachines = context.SyntaxProvider.ForAttributeWithMetadataName(typeof(GenerateStateMapAttribute<,>).FullName,
                predicate: static (node, _) => true,
                transform: static (ctx, _) => GetServiceDetail(ctx)).Collect();

            var combined = IncrementalValueProviderExtensions.Combine( additionalFiles, stateMachines);

            context.RegisterSourceOutput(combined, Execute);
        }
        catch (Exception ex) {
            throw new Exception($"{nameof(Initialize)} : {ex.Message} - {ex.StackTrace}", ex);
        }
    }

    private void Execute(SourceProductionContext context, (ImmutableArray<ConfigurationFile> configurationFiles, ImmutableArray<StateMachineDescription> stateMachines) tuple)
    {
        foreach (var stateMachine in tuple.stateMachines)
        {
            var configFile = tuple.configurationFiles.SingleOrDefault(x => x.Path == stateMachine.Path);

            if (configFile == null)
               throw new System.Exception($"State Machine configuration `{stateMachine.Path}` could not be found ");

            var source = CSharpClientBuilder.Build(stateMachine, configFile.Code);

            context.AddSource($"StateMachines/{stateMachine.Classname}.g.cs", source);
        }
    }
}