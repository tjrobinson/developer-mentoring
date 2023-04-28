using AutoMapper;
using FluentAssertions;
using NUnit.Framework;

namespace TypeMapping;

public class Employee
{
    public string Name { get; set; }
    public int Salary { get; set; }
    public string Address { get; set; }
    public string Department { get; set; }
}

public class EmployeeDto
{
    public string DisplayName { get; set; }
    public string Address { get; set; }
    public string Department { get; set; }
}

public class Tests
{
    [Test]
    public void ManualMapping()
    {
        var employee = new Employee
        {
            Name = "Bob",
            Salary = 20000,
            Address = "London",
            Department = "IT"
        };
        
        var employeeDto = new EmployeeDto
        {
            DisplayName = employee.Name,
            Address = employee.Address,
            Department = employee.Department
        };

        employeeDto.Should().BeEquivalentTo(employee);
    }
    
    [Test]
    public void AutoMapperMapping()
    {
        var employee = new Employee
        {
            Name = "Bob",
            Salary = 20000,
            Address = "London",
            Department = "IT"
        };

        var mapperConfiguration = new MapperConfiguration(config => config.CreateMap<Employee, EmployeeDto>()
            .ForMember(m => m.DisplayName, expression => expression.MapFrom(c => c.Name)));

        var mapper = new Mapper(mapperConfiguration);

        var employeeDto = mapper.Map<EmployeeDto>(employee);
        
        employeeDto.Should().BeEquivalentTo(employee);
    }
}

