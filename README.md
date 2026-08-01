# CB4 - Can't Be Bothered to Build a Builder

Simple library to use code generation to create builder pattern classes for simple data classes or records.

## Example data class
```C#
public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string StudentId { get; set; }
    public double GradePointAverage { get; set; }
}
```

## Example generated code
```C#
public class StudentBuilder
{
    private Student _student = new();

    public StudentBuilder WithFirstName(string firstName)
    {
        _student.FirstName = firstName;
        return this;
    }

    public StudentBuilder WithLastName(string firstName)
    {
        _student.LastName = lastName;
        return this;
    }

    public StudentBuilder WithStudentId(string studentId)
    {
        _student.StudentId = studentId;
        return this;
    }

    public StudentBuilder WithGradePointAverage(double gradePointAverage)
    {
        _student.GradePointAverage = gradePointAverage;
        return this;
    }

    public Student Build() => _student;
    
    public static implicit operator Student(StudentBuilder builder) => builder.Build();
}
```

## Example usage
```C#
public class StudentTests
{
    public void TestGpa()
    {
        var gpa = 3.45;
        var student = new StudentBuilder()
            .WithGradePointAverage(gpa)
            .Build();

        Assert.Equal(student.GradePointAvergage, gpa);
    }
}
```