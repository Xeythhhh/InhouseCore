using System.Reflection;

using Application;
using Application.UnitTests;

using Build;

using Domain;
using Domain.UnitTests;

using Host;
using Host.Client;

using Infrastructure;
using Infrastructure.UnitTests;

using NetArchTest.Rules;

using Presentation.Blazor;
using Presentation.Blazor.UnitTests;
using Presentation.Discord;
using Presentation.Discord.UnitTests;

using SharedKernel;

using Xunit.Abstractions;

namespace Tests.ArchitectureTests;
public abstract class ArchitectureBaseTest
{
    protected static readonly Assembly Application = ApplicationAssembly.Reference;
    protected static readonly Assembly Domain = DomainAssembly.Reference;
    protected static readonly Assembly Infrastructure = InfrastructureAssembly.Reference;
    protected static readonly Assembly PresentationDiscord = PresentationDiscordAssembly.Reference;
    protected static readonly Assembly PresentationBlazor = PresentationBlazorAssembly.Reference;
    protected static readonly Assembly Host = HostAssembly.Reference;
    protected static readonly Assembly Client = ClientAssembly.Reference;
    protected static readonly Assembly Build = BuildAssembly.Reference;
    protected static readonly Assembly SharedKernel = SharedKernelAssembly.Reference;

    protected static Assembly[] Presentation => [PresentationBlazor, PresentationDiscord];

    protected static Assembly[] Core => [Application, Domain];
    protected static Assembly[] External => [Infrastructure, PresentationDiscord, PresentationBlazor];
    protected static Assembly[] Hosts => [Host, Client];
    protected static Assembly[] Tests => [
        TestsAssembly.Reference,
        ApplicationUnitTestsAssembly.Reference,
        DomainUnitTestsAssembly.Reference,
        InfrastructureUnitTestsAssembly.Reference,
        PresentationDiscordUnitTestsAssembly.Reference,
        PresentationBlazorUnitTestsAssembly.Reference];

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
