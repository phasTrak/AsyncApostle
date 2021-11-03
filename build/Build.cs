using System.IO.Compression;
using JetBrains.Annotations;
using Nuke.Common.Tools.DotNet;
using Nuke.Core;
using Nuke.Core.Utilities.Collections;
using static System.IO.Directory;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Core.IO.FileSystemTasks;
using static Nuke.Core.IO.PathConstruction;

class Build : NukeBuild
{
    #region properties

    // Auto-injection fields:

    // [GitVersion] readonly GitVersion GitVersion;
    // Semantic versioning. Must have 'GitVersion.CommandLine' referenced.

    // [GitRepository] readonly GitRepository GitRepository;
    // Parses origin, branch name and head from git config.

    // [Parameter] readonly string MyGetApiKey;
    // Returns command-line arguments and environment variables.
    public override AbsolutePath ArtifactsDirectory => SolutionDirectory / "packages";

    [NotNull]
    Target Clean =>
        _ => _
            .Executes(() =>
                      {
                          EnsureCleanDirectory(ArtifactsDirectory);

                          GlobDirectories(SolutionDirectory / "AsyncApostle", "**/bin", "**/obj")
                              .ForEach(EnsureCleanDirectory);
                      });

    [NotNull]
    Target Compile =>
        _ => _
             .DependsOn(Restore)
             .Executes(() =>
                       {
                           DotNetBuild(s => DefaultDotNetBuild);
                       });

    [NotNull]
    Target Pack =>
        _ => _
             .DependsOn(Compile)
             .Executes(() =>
                       {
                           //TODO: DeleteDirectory not work, and move to Clean
                           if (Exists(ArtifactsDirectory))
                               Delete(ArtifactsDirectory, true);

                           DotNetPack(s => DefaultDotNetPack
                                           .SetOutputDirectory(ArtifactsDirectory)
                                           .DisableIncludeSymbols()
                                           .SetProject("AsyncApostle/AsyncApostle.csproj"));

                           DotNetPack(s => DefaultDotNetPack
                                           .SetOutputDirectory(SolutionDirectory / "Rider" / "AsyncApostle.Rider")
                                           .DisableIncludeSymbols()
                                           .SetProject("AsyncApostle/AsyncApostle.Rider.csproj"));

                           ZipFile.CreateFromDirectory(SolutionDirectory / "Rider",
                                                       ArtifactsDirectory / "AsyncApostle.Rider.zip");
                       });

    [NotNull]
    Target Restore =>
        _ => _
             .DependsOn(Clean)
             .Executes(() =>
                       {
                           DotNetRestore(s => DefaultDotNetRestore.SetProjectFile("AsyncApostle/AsyncApostle.csproj"));
                           DotNetRestore(s => DefaultDotNetRestore.SetProjectFile("AsyncApostle/AsyncApostle.Rider.csproj"));
                           DotNetRestore(s => DefaultDotNetRestore.SetProjectFile("AsyncApostle.Tests/AsyncApostle.Tests.csproj"));
                           DotNetRestore(s => DefaultDotNetRestore.SetProjectFile("AsyncApostle.Tests/AsyncApostle.Rider.Tests.csproj"));
                       });

    #endregion

    #region methods

    // Console application entry. Also defines the default target.
    public static int Main() => Execute<Build>(x => x.Compile);

    #endregion
}
