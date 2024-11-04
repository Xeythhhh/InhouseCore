using System.Reflection;
using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

[assembly: InternalsVisibleTo("SharedKernel.UnitTests")]
namespace SharedKernel;
/// <summary> Class to reference the SharedKernel <see cref="Assembly"/> </summary>
public static class SharedKernelAssembly
{
    /// <summary> A Reference to the SharedKernel <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(SharedKernelAssembly).Assembly;
}