using System.Reflection;
using FluentAssertions;
using Mediator.Domain;
using Mediator.Domain.Abstractions.Events;
using NetArchTest.Rules;
using Xunit;

namespace ArchitectureTests.Domain;

public class DomainTests : BaseTest
{
    [Fact]
    public void DomainEvents_Should_BeSealed()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEvents_Should_HaveDomainEventsPostfix()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith("DomainEvent")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Entities_Should_BeSealed()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(MediatorEntity))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Entities_Should_HavePrivateParameterlessConstructor()
    {
        IEnumerable<Type> entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(MediatorEntity))
            .GetTypes();

        var failingTypes = new List<Type>();
        foreach (Type entityType in entityTypes)
        {
            ConstructorInfo[] constructors = entityType
                .GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

            if (!constructors.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
            {
                failingTypes.Add(entityType);
            }
        }

        failingTypes.Should().BeEmpty(
            $"The following entities are missing private parameters: " +
            $"{string.Join(", ", failingTypes)}");
    }

    [Fact]
    public void Entities_Should_HaveStaticErrorsClass()
    {
        IEnumerable<Type> entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(MediatorEntity))
            .GetTypes();

        var missingErrorClasses = new List<string>();

        foreach (Type entityType in entityTypes)
        {
            string expectedErrorClassName = $"{entityType.Name}Errors";

            Type? errorClassType = DomainAssembly.GetTypes()
                .FirstOrDefault(t => t.Name == expectedErrorClassName 
                    && t.IsClass && t.IsAbstract && t.IsSealed);

            if (errorClassType == null)
            {
                missingErrorClasses.Add(expectedErrorClassName);
            }
        }

        missingErrorClasses.Should().BeEmpty(
            $"The following entities are missing static Errors classes: " +
            $"{string.Join(", ", missingErrorClasses)}");
    }

    [Fact]
    public void Entities_Should_HaveCorrespondingRepositoryInterface()
    {
        IEnumerable<Type> entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(MediatorEntity))
            .GetTypes();

        var missingRepositories = new List<string>();

        foreach (Type entityType in entityTypes)
        {
            string expectedInterfaceName = $"I{entityType.Name}Repository";

            Type? repositoryInterface = DomainAssembly.GetTypes()
                .FirstOrDefault(t => t.IsInterface && t.Name == expectedInterfaceName);

            if (repositoryInterface == null)
            {
                missingRepositories.Add(expectedInterfaceName);
            }
        }

        missingRepositories.Should().BeEmpty(
            $"The following entities are missing their corresponding repository interfaces: " +
            $"{string.Join(", ", missingRepositories)}");
    }
}
