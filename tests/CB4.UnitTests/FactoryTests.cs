using System;
using CB4.ExternalExample.Models;

namespace CB4.UnitTests;

public class FactoryTests
{
    [Fact]
    public void TestPocoWithoutDefault()
    {
        const string firstName1 = "Joe";
        const string firstName2 = "Jane";
        var factory = new StudentBuilderFactory();
            
        var student1 = factory.Create()    
            .WithFirstName(firstName1)
            .Build();
        var student2 = factory.Create()
            .WithFirstName(firstName2)
            .Build();
        
        Assert.Equal(firstName1, student1.FirstName);
        Assert.Equal(firstName2, student2.FirstName);
        Assert.NotEqual(student1, student2);
    }
    
    [Fact]
    public void TestPocoWithConstructorDefault()
    {
        const string firstName1 = "Joe";
        const string firstName2 = "Jane";
        var defaultValue = new Student
        {
            FirstName = "Jim",
            LastName = "Doe",
            StudentId = "12345",
            GradePointAverage = 2.01,
        };
        var factory = new StudentBuilderFactory(defaultValue);
        
        var student1 = factory.Create()    
            .WithFirstName(firstName1)
            .Build();
        var student2 = factory.Create()
            .WithFirstName(firstName2)
            .Build();
        
        Assert.Equal(firstName1, student1.FirstName);
        Assert.Equal(defaultValue.LastName, student1.LastName);
        Assert.Equal(defaultValue.StudentId, student1.StudentId);
        Assert.Equal(defaultValue.GradePointAverage, student1.GradePointAverage);
        Assert.NotEqual(defaultValue.FirstName, student1.FirstName);
        
        Assert.Equal(firstName2, student2.FirstName);
        Assert.Equal(defaultValue.LastName, student2.LastName);
        Assert.Equal(defaultValue.StudentId, student2.StudentId);
        Assert.Equal(defaultValue.GradePointAverage, student2.GradePointAverage);
        Assert.NotEqual(defaultValue.FirstName, student2.FirstName);
        
        Assert.NotEqual(student1, student2);
    }
    
    [Fact]
    public void TestPocoWithCreationDefault()
    {
        const string firstName1 = "Joe";
        const string firstName2 = "Jane";
        var defaultValue1 = new Student
        {
            FirstName = "John",
            LastName = "Doh",
            StudentId = "23456",
            GradePointAverage = 4.12,
        };
        var defaultValue2 = new Student
        {
            FirstName = "Jim",
            LastName = "Doe",
            StudentId = "12345",
            GradePointAverage = 2.01,
        };
        var factory = new StudentBuilderFactory(defaultValue1);
        
        var student1 = factory.Create()    
            .WithFirstName(firstName1)
            .Build();
        var student2 = factory.Create(defaultValue2)
            .WithFirstName(firstName2)
            .Build();
        
        Assert.Equal(firstName1, student1.FirstName);
        Assert.Equal(defaultValue1.LastName, student1.LastName);
        Assert.Equal(defaultValue1.StudentId, student1.StudentId);
        Assert.Equal(defaultValue1.GradePointAverage, student1.GradePointAverage);
        Assert.NotEqual(defaultValue1.FirstName, student1.FirstName);
        
        Assert.Equal(firstName2, student2.FirstName);
        Assert.Equal(defaultValue2.LastName, student2.LastName);
        Assert.Equal(defaultValue2.StudentId, student2.StudentId);
        Assert.Equal(defaultValue2.GradePointAverage, student2.GradePointAverage);
        Assert.NotEqual(defaultValue2.FirstName, student2.FirstName);
        
        Assert.NotEqual(student1, student2);
    }
    
    [Fact]
    public void TestRecordWithConstructorDefault()
    {
        const string name1 = "Bubbles";
        const string name2 = "Sparkles";
        var defaultValue = new Pet(
            "Cassie",
            3,
            new DateTime(2026, 1, 2));
        
        var factory = new PetBuilderFactory(defaultValue);
        
        var pet1 = factory.Create()
            .WithName(name1)
            .Build();
        var pet2 = factory.Create()
            .WithName(name2)
            .Build();
        
        Assert.Equal(name1, pet1.Name);
        Assert.NotEqual(defaultValue.Name, pet1.Name);
        
        Assert.Equal(name2, pet2.Name);
        Assert.NotEqual(defaultValue.Name, pet2.Name);
        
        Assert.NotEqual(pet1, pet2);
    }

    [Fact]
    public void TestRecordWithCreationDefault()
    {
        const string name1 = "Bubbles";
        const string name2 = "Sparkles";
        var defaultValue1 = new Pet(
            "Cassie",
            3,
            new DateTime(2026, 1, 2));
        var defaultValue2 = new Pet(
            "Casey",
            4,
            new DateTime(2026, 1, 3));
        
        var factory = new PetBuilderFactory(defaultValue1);
        
        var pet1 = factory.Create()
            .WithName(name1)
            .Build();
        var pet2 = factory.Create(defaultValue2)
            .WithName(name2)
            .Build();
        
        Assert.Equal(name1, pet1.Name);
        Assert.Equal(defaultValue1.Age, pet1.Age);
        Assert.Equal(defaultValue1.VaccinationDate, pet1.VaccinationDate);
        Assert.NotEqual(defaultValue1.Name, pet1.Name);
        
        Assert.Equal(name2, pet2.Name);
        Assert.Equal(defaultValue2.Age, pet2.Age);
        Assert.Equal(defaultValue2.VaccinationDate, pet2.VaccinationDate);
        Assert.NotEqual(defaultValue2.Name, pet2.Name);
        
        Assert.NotEqual(pet1, pet2);
    }
}