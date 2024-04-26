using System.Reflection;

using Build;
using SharedKernel;
using Host;
using Host.Client;
using Application;
using Application.UnitTests;
using Domain;
using Domain.UnitTests;
using Infrastructure;
using Presentation;

using Xunit.Abstractions;
using NetArchTest.Rules;

namespace Tests.ArchitectureTests;
public abstract class ArchitectureBaseTest
{
    protected static readonly Assembly Application = ApplicationAssembly.Reference;
    protected static readonly Assembly Domain = DomainAssembly.Reference;
    protected static readonly Assembly Infrastructure = InfrastructureAssembly.Reference;
    protected static readonly Assembly Presentation = PresentationAssembly.Reference;
    protected static readonly Assembly Host = HostAssembly.Reference;
    protected static readonly Assembly Client = ClientAssembly.Reference;
    protected static readonly Assembly Build = BuildAssembly.Reference;
    protected static readonly Assembly SharedKernel = SharedKernelAssembly.Reference;

    protected static Assembly[] Core => [Application, Domain];
    protected static Assembly[] External => [Infrastructure, Presentation];
    protected static Assembly[] Hosts => [Host, Client];
    protected static Assembly[] Tests => [
        TestsAssembly.Reference,
        ApplicationUnitTestsAssembly.Reference,
        DomainUnitTestsAssembly.Reference];

    /// <summary>Helper output method to extract information about the <see cref="TestResult"/></summary>
    protected static void OutputTestResults(ITestOutputHelper output, TestResult testResult)
    {
        if (!testResult.IsSuccessful)
        {
            output.WriteLine($"""

                Types that failed to meet the conditions:
                -----------------------------------------
                {string.Join("\n", testResult.FailingTypes.Select(t => $"{t.Name}\n    - {t.Explanation}"))}
                """);
        }

        output.WriteLine($"""

            Types that passed the predicates:
            ---------------------------------
            {string.Join("\n", testResult.SelectedTypesForTesting.Select(t => t.Name))}
            """);

    }
}
