using System;
using Buildable.Attributes;

namespace CB4.UnitTests.Models;

[Buildable]
public record Pet(
    string Name,
    int Age,
    DateTime VaccinationDate);
