using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CB4.Emitters;
using CB4.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;


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
                        .Select(p => new BuildableModelProperty(
                            p.Name,
                            p.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)));

                    return new BuildableModel(
                        classSymbol.ContainingNamespace.ToDisplayString(),
                        syntaxContext.TargetSymbol.Name,
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
                    spc.AddSource($"{model.Name}Builder.g.cs", PocoEmitter.Emit(model));
                }
            });
    }
}
