using CB4.UnitTests.ExampleClasses;

namespace CB4.UnitTests;

public class PocoTests
{
    [Fact]
    public void TestFirstName()
    {
        var firstName = "Foo";
        var student = new StudentBuilder()
            .WithFirstName(firstName)
            .Build();

        Assert.Equal(student.FirstName, firstName);
    }
    
    [Fact]
    public void TestLastName()
    {
        var lastName = "Bar";
        var student = new StudentBuilder()
            .WithLastName(lastName)
            .Build();

        Assert.Equal(student.LastName, lastName);
    }
    
    [Fact]
    public void TestStudentId()
    {
        var id = "12345";
        var student = new StudentBuilder()
            .WithStudentId(id)
            .Build();

        Assert.Equal(student.StudentId, id);
    }
    
    [Fact]
    public void TestGradePointAverage()
    {
        var gpa = 3.45;
        var student = new StudentBuilder()
            .WithGradePointAverage(gpa)
            .Build();

        Assert.Equal(student.GradePointAverage, gpa);
    }
}
