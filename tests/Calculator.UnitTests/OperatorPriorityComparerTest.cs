﻿using System.Reflection;
using EquationCalculator.Core.Operators;
using FluentAssertions;
using Xunit;

namespace EquationCalculator.UnitTests;

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
                           type is { IsAbstract: false, IsClass: true })
            .Select(type => new object[] { type });
    }
}
