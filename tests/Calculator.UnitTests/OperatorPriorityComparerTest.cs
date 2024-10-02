using System.Reflection;
using Byndyusoft.Calculator.Core.Operators;
using FluentAssertions;
using Xunit;

namespace Byndyusoft.Calculator.UnitTests;

public sealed class OperatorPriorityComparerTest
{
    [Theory]
    [MemberData(nameof(GetAllOperatorTypes))]
    public void ShouldGetOperatorPriorityForAllOperatorTypes(Type operatorType)
    {
        // Arrange

        // Act
        var getOperatorPriority = () => OperatorPriorityComparer.GetTokenPriority(operatorType);

        // Assert
        getOperatorPriority.Should().NotThrow($"«{operatorType.FullName}» should be resolved in «{nameof(OperatorPriorityComparer)}»");
    }

    public static IEnumerable<object[]> GetAllOperatorTypes()
    {
        return Assembly
            .GetAssembly(typeof(IOperatorToken))!
            .GetTypes()
            .Where(type => type.IsAssignableTo(typeof(IOperatorToken)) &&
                           type is { IsAbstract: false, IsValueType: true })
            .Select(type => new object[] { type });
    }
}
