using System.Collections;
using System.Reflection;

using Microsoft.CodeAnalysis;
using FluentAssertions;
using NetArchTest.Rules;
using Xunit.Abstractions;

namespace Tests.ArchitectureTests;
public class LayerTests(ITestOutputHelper output)
    : ArchitectureBaseTest
{
    [Theory]
    [ClassData(typeof(Architecture))]
    public void Layer_ShouldNotDependOn_OtherLayers(Architecture.Rule rule)
    {
        // Arrange
        PredicateList types = Types.InAssembly(rule.AssemblyUnderTest)
            .That().ResideInNamespace(rule.AssemblyUnderTest.GetName().Name);

        // Act
        TestResult testResult = types
            .ShouldNot().HaveDependencyOnAny(rule.NonDependencies)
            .GetResult();

        // Assert
        OutputTestResults(output, testResult);
        testResult.IsSuccessful.Should().BeTrue();
    }

    public class Architecture : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Rule(Domain,
                [Build, Application, .. External, .. Hosts, .. Tests]) };

            yield return new object[] { new Rule(Application,
                [Build, .. External, .. Hosts, .. Tests]) };

            yield return new object[] { new Rule(Infrastructure,
                [Build, Presentation, .. Hosts, .. Tests]) };

            yield return new object[] { new Rule(Presentation,
                [Build, Infrastructure, .. Hosts, .. Tests]) };

            yield return new object[] { new Rule(Host,
                [Build, .. Tests]) };

            yield return new object[] { new Rule(Client,
                [Build, .. Tests]) };

            yield return new object[] { new Rule(SharedKernel,
                [Build, .. Core, .. External, .. Tests]) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public class Rule(Assembly assembly, Assembly[] nonDependencies)
        {
            public Assembly AssemblyUnderTest { get; } = assembly;
            public string?[] NonDependencies { get; } = nonDependencies.Select(a => a.GetName().Name).ToArray();

            public override string ToString() => $"\n{AssemblyUnderTest.GetName().Name} should not have dependency on {string.Join(", ", NonDependencies)}";
        }
    }
}