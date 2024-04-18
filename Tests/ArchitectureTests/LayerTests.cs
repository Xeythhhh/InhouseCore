using System.Collections;
using System.Reflection;

using Microsoft.CodeAnalysis;
using FluentAssertions;
using NetArchTest.Rules;

namespace Tests.ArchitectureTests;
public class LayerTests : ArchitectureBaseTest
{
    [Theory]
    [ClassData(typeof(DataSource))]
    public void Layer_ShouldNotDependOn_OtherLayers(DataSource.Rule rule) =>
        Types.InAssembly(rule.Assembly)
            .ShouldNot().HaveDependencyOnAny(rule.NonDependencies)
            .GetResult().IsSuccessful.Should().BeTrue();

    public class DataSource : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Rule(Domain, [Build, Application, Infrastructure, Presentation, Host, Client, Tests]) };
            yield return new object[] { new Rule(Application, [Build, Infrastructure, Presentation, Host, Client, Tests]) };
            yield return new object[] { new Rule(Infrastructure, [Build, Presentation, Host, Client, Tests]) };
            yield return new object[] { new Rule(Presentation, [Build, Infrastructure, Host, Client, Tests]) };
            yield return new object[] { new Rule(Host, [Build, Tests]) };
            yield return new object[] { new Rule(Client, [Build, Tests]) };
            yield return new object[] { new Rule(SharedKernel, [Build, Domain, Application, Infrastructure, Presentation, Host, Client, Tests]) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public class Rule(Assembly assembly, Assembly[] nonDependencies)
        {
            public Assembly Assembly { get; } = assembly;
            public string?[] NonDependencies { get; } = nonDependencies.Select(a => a.GetName().Name).ToArray();

            public override string ToString() =>
                $"\n{Assembly.GetName().Name} should not have dependency on {string.Join(", ", NonDependencies)}";
        }
    }
}