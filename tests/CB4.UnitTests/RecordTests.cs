using System;
using CB4.UnitTests.Models;

namespace CB4.UnitTests;

public class RecordTests
{
    [Fact]
    public void TestName()
    {
        const string name = "Charlie";
        var pet = new PetBuilder()
            .WithName(name)
            .Build();
        
        Assert.Equal(name, pet.Name);
    }
    
    [Fact]
    public void TestAge()
    {
        const int age = 4;
        var pet = new PetBuilder()
            .WithAge(age)
            .Build();
        
        Assert.Equal(age, pet.Age);
    }
    
    [Fact]
    public void TestVaccinationDate()
    {
        var vaccinationDate = new DateTime(2026, 1, 1);
        var pet = new PetBuilder()
            .WithVaccinationDate(vaccinationDate)
            .Build();
        
        Assert.Equal(vaccinationDate, pet.VaccinationDate);
    }

    [Fact]
    public void TestDefaults()
    {
        var name = "Buddy";
        var defaults = new Pet(
            "Charlie",
            4,
            new DateTime(2026, 1, 1));
        var pet = new PetBuilder(defaults)
            .WithName(name)
            .Build();
        
        Assert.Equal(defaults.Age, pet.Age);
        Assert.Equal(defaults.VaccinationDate, pet.VaccinationDate);
        Assert.NotEqual(defaults.Name, pet.Name);
        Assert.Equal(name, pet.Name);
    }
}
