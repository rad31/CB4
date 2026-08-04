using System.Linq;
using System.Threading;
using CB4.Emitters;
using CB4.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace CB4;

[Generator(LanguageNames.CSharp)]
public class BuilderGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(c => c.AddSource(
            $"{BuildableAttributeEmitter.FullyQualifiedAttributeName}.g.cs",
            BuildableAttributeEmitter.Emit()));
        
        context.RegisterPostInitializationOutput(c => c.AddSource(
            $"{FactoryAttributeEmitter.FullyQualifiedAttributeName}.g.cs",
            FactoryAttributeEmitter.Emit()));

        var buildables = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                BuildableAttributeEmitter.FullyQualifiedAttributeName,
                predicate: static (_, _) => true,
                transform: FromBuildable)
            .Collect();
        
        var factoryBuildables = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                $"{FactoryAttributeEmitter.FullyQualifiedAttributeName}`1",
                predicate: IsPartialClass,
                transform: FromFactory)
            .Collect();
        
        context.RegisterSourceOutput(
            buildables,
            static (spc, source) =>
            {
                foreach (var model in source)
                {
                    spc.CancellationToken.ThrowIfCancellationRequested();
                    spc.AddSource($"{model.Ns}.{model.Name}Builder.Proxy.g.cs", ProxyEmitter.Emit(model));
                    spc.AddSource($"{model.Ns}.{model.Name}Builder.g.cs", BuilderEmitter.Emit(model));
                }
            });
        
        context.RegisterSourceOutput(
            factoryBuildables,
            static (spc, source) =>
            {
                foreach (var model in source)
                {
                    spc.CancellationToken.ThrowIfCancellationRequested();
                    spc.AddSource($"{model.Ns}.{model.Name}Builder.Proxy.g.cs", ProxyEmitter.Emit(model));
                    spc.AddSource($"{model.Ns}.{model.Name}Builder.g.cs", BuilderEmitter.Emit(model));
                    spc.AddSource($"{model.Ns}.{model.FactoryName}.g.cs", FactoryEmitter.Emit(model));
                }
            });
    }

    private static BuildableModel FromBuildable(GeneratorAttributeSyntaxContext syntaxContext, CancellationToken cancellationToken)
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
            null,
            constructorType,
            properties);
    }
    
    private static BuildableModel FromFactory(GeneratorAttributeSyntaxContext syntaxContext, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var attributeData = syntaxContext.Attributes.First();
        var classSymbol = (INamedTypeSymbol)(attributeData!.AttributeClass.TypeArguments[0]);
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
            classSymbol.Name,
            syntaxContext.TargetSymbol.Name,
            constructorType,
            properties);
    }
    
    private static bool IsPartialClass(SyntaxNode node, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return node is ClassDeclarationSyntax classDecl
            && classDecl.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword));
    }
}
