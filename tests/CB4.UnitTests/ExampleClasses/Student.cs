using Buildable.Attributes;

namespace CB4.UnitTests.ExampleClasses;

[Buildable]
public class Student
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? StudentId { get; set; }
    public double? GradePointAverage { get; set; }
}
