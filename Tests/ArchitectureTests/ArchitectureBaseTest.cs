using System.Reflection;

using Api;
using Api.UnitTests;

using Application;
using Application.UnitTests;

using Build;

using Domain;
using Domain.UnitTests;

using Infrastructure;
using Infrastructure.UnitTests;

using NetArchTest.Rules;

using Presentation.Discord;
using Presentation.Discord.UnitTests;

using SharedKernel;

using WebApp;
using WebApp.UnitTests;

using Xunit.Abstractions;

namespace Tests.ArchitectureTests;
public abstract class ArchitectureBaseTest
{
    protected static readonly Assembly Application = ApplicationAssembly.Reference;
    protected static readonly Assembly Domain = DomainAssembly.Reference;
    protected static readonly Assembly Infrastructure = InfrastructureAssembly.Reference;
    protected static readonly Assembly PresentationDiscord = PresentationDiscordAssembly.Reference;
    protected static readonly Assembly Api = ApiAssembly.Reference;
    protected static readonly Assembly BlazorClient = WebAppAssembly.Reference;
    protected static readonly Assembly Build = BuildAssembly.Reference;
    protected static readonly Assembly SharedKernel = SharedKernelAssembly.Reference;

    protected static Assembly[] Core => [Application, Domain];
    protected static Assembly[] External => [Infrastructure, PresentationDiscord];
    protected static Assembly[] Hosts => [Api, BlazorClient];
    protected static Assembly[] Tests => [
        TestsAssembly.Reference,
        ApiUnitTestsAssembly.Reference,
        WebAppUnitTestsAssembly.Reference,
        ApplicationUnitTestsAssembly.Reference,
        DomainUnitTestsAssembly.Reference,
        InfrastructureUnitTestsAssembly.Reference,
        PresentationDiscordUnitTestsAssembly.Reference];

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
