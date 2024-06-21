using Nuke.Common;

namespace Build;
public class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
#pragma warning disable IDE0052 // Remove unread private members
#pragma warning disable RCS1213 // Remove unused member declaration
    private readonly Configuration Configuration = IsLocalBuild
#pragma warning restore RCS1213 // Remove unused member declaration
#pragma warning restore IDE0052 // Remove unread private members
        ? Configuration.Debug
        : Configuration.Release;

#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable RCS1213 // Remove unused member declaration
    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
        });
#pragma warning restore RCS1213 // Remove unused member declaration
#pragma warning restore IDE0051 // Remove unused private members

    Target Restore => _ => _
        .Executes(() =>
        {
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
        });
}
