using FluentAssertions;
using Mediator.Application.Abstractions.Messaging;
using NetArchTest.Rules;
using Xunit;

namespace ArchitectureTests.Application;
public class ApplicationTests : BaseTest
{
    [Fact]
    public void CommandHandler_Should_BeSealedAndInternal()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .HaveNameEndingWith("CommandHandler")
            .GetTypes()
            .Where(type => type.IsSealed && !type.IsPublic)
            .ToList();

        result.Should().NotBeEmpty("All CommandHandler implementations " +
            "should be sealed and internal.");
    }

    [Fact]
    public void Command_Should_BeSealedAndPublic()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .GetTypes()
            .Where(type => type.GetInterfaces()
                .Any(i => i == typeof(ICommand<Guid>))
            ).Where(type => type.IsSealed && type.IsPublic)

            .ToList();

        result.Should().NotBeEmpty("All ICommand<> implementations " +
            "should be sealed and internal.");
    }
}
