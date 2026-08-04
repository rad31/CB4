using System.Collections.Generic;

namespace CB4.Models;

public record struct BuildableModel(
    string Ns,
    string Name,
    string? FactoryName,
    ConstructorType ConstructorType,
    IEnumerable<BuildableModelProperty> Properties);
    