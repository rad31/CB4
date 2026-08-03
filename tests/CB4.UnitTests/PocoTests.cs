using CB4.UnitTests.Models;

namespace CB4.UnitTests;

public class PocoTests
{
    [Fact]
    public void TestFirstName()
    {
        const string firstName = "Foo";
        var student = new StudentBuilder()
            .WithFirstName(firstName)
            .Build();

        Assert.Equal(firstName, student.FirstName);
    }
    
    [Fact]
    public void TestLastName()
    {
        const string lastName = "Bar";
        var student = new StudentBuilder()
            .WithLastName(lastName)
            .Build();

        Assert.Equal(lastName, student.LastName);
    }
    
    [Fact]
    public void TestStudentId()
    {
        const string id = "12345";
        var student = new StudentBuilder()
            .WithStudentId(id)
            .Build();

        Assert.Equal(id, student.StudentId);
    }
    
    [Fact]
    public void TestGradePointAverage()
    {
        const double gpa = 3.45;
        var student = new StudentBuilder()
            .WithGradePointAverage(gpa)
            .Build();

        Assert.Equal(gpa, student.GradePointAverage);
    }
}
