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

    [Fact]
    public void TestDefaults()
    {
        const double gpa = 4.20;
        const string id = "23456";
        var defaults = new Student
        {
            FirstName = "Foo",
            LastName = "Bar",
            GradePointAverage = 3.45,
            StudentId = "12345",
        };
        var student = new StudentBuilder(defaults)
            .WithGradePointAverage(gpa)
            .WithStudentId(id)
            .Build();
        
        Assert.Equal(defaults.FirstName, student.FirstName);
        Assert.Equal(defaults.LastName, student.LastName);
        Assert.NotEqual(defaults.GradePointAverage, student.GradePointAverage);
        Assert.NotEqual(defaults.StudentId, student.StudentId);
        Assert.Equal(gpa, student.GradePointAverage);
        Assert.Equal(id, student.StudentId);
    }
}
