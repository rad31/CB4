using System.Linq;
using CB4.Emitters;
using CB4.Models;
using Microsoft.CodeAnalysis;


namespace CB4;

[Generator(LanguageNames.CSharp)]
public class BuilderGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(c => c.AddSource(
            $"{BuildableAttributeEmitter.AttributeName}.g.cs",
            BuildableAttributeEmitter.Emit()));

        var models = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                BuildableAttributeEmitter.FullyQualifiedAttributeName,
                predicate: static (_, _) => true,
                transform: static (syntaxContext, cancellationToken) =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var classSymbol = (INamedTypeSymbol)syntaxContext.TargetSymbol;
                    var properties = classSymbol.GetMembers().OfType<IPropertySymbol>()
                        .Where(p => !classSymbol.IsRecord || p.Name != "EqualityContract")
                        .Select(p => new BuildableModelProperty(
                            p.Name,
                            p.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)));
                    var constructorType = classSymbol.Constructors.Any(c => !c.IsStatic && c.Parameters.IsEmpty)
                        ? ConstructorType.WithoutArguments
                        : ConstructorType.WithArguments;

                    return new BuildableModel(
                        classSymbol.ContainingNamespace.ToDisplayString(),
                        syntaxContext.TargetSymbol.Name,
                        constructorType,
                        properties);
                })
            .Collect();
        
        context.RegisterSourceOutput(
            models,
            static (spc, source) =>
            {
                foreach (var model in source)
                {
                    spc.CancellationToken.ThrowIfCancellationRequested();
                    spc.AddSource($"{model.Name}Builder.Proxy.g.cs", ProxyEmitter.Emit(model));
                    spc.AddSource($"{model.Name}Builder.g.cs", BuilderEmitter.Emit(model));
                }
            });
    }
}
