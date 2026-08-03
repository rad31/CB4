using System.Collections.Generic;

namespace CB4.Models;

public record struct BuildableModel(
    string Ns,
    string Name,
    IEnumerable<BuildableModelProperty> Properties);