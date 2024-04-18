using System.Reflection;

using Build;
using Host;
using Host.Client;
using SharedKernel;
using Application;
using Domain;
using Infrastructure;
using Presentation;

namespace Tests.ArchitectureTests;
public abstract class ArchitectureBaseTest
{
    protected static readonly Assembly Build = BuildAssembly.Reference;
    protected static readonly Assembly Application = ApplicationAssembly.Reference;
    protected static readonly Assembly Domain = DomainAssembly.Reference;
    protected static readonly Assembly Infrastructure = InfrastructureAssembly.Reference;
    protected static readonly Assembly Presentation = PresentationAssembly.Reference;
    protected static readonly Assembly Host = HostAssembly.Reference;
    protected static readonly Assembly Client = ClientAssembly.Reference;
    protected static readonly Assembly SharedKernel = SharedKernelAssembly.Reference;
    protected static readonly Assembly Tests = TestsAssembly.Reference;
}
