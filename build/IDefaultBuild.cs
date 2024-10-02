using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

interface IDefaultBuild
{
    Target Restore => _ => _
        .Executes(() =>
        {
            Log.Information("Start packages restore...");
            DotNetRestore();
        });

    Target Build => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            Log.Information("Start solution build...");
            DotNetBuild(settings =>
                settings
                    .SetNoRestore(true)
                    .SetConfiguration("Release"));
        });

    Target Test => _ => _
        .DependsOn(Build)
        .Executes(() =>
        {
            Log.Information("Run tests...");
            DotNetTest(settings =>
                settings
                    .SetNoBuild(true)
                    .SetNoRestore(true)
                    .SetConfiguration("Release")
                    .SetVerbosity(DotNetVerbosity.detailed));
        });

    Target Formatting => _ => _
        .DependsOn(Test)
        .Executes(() =>
        {
            Log.Information("Check code formatting...");
            DotNetFormat(settings =>
                settings
                    .SetVerifyNoChanges(true)
                    .SetExclude("./build"));
        });
}
