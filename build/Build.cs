using Nuke.Common.Tools.DotNet;
using Nuke.Core;
using Nuke.Core.Utilities.Collections;
using static System.IO.Compression.ZipFile;
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

   Target Clean =>
      _ => _.Executes(() =>
                      {
                         EnsureCleanDirectory(ArtifactsDirectory);

                         GlobDirectories(SolutionDirectory / "AsyncApostle", "**/bin", "**/obj")
                           .ForEach(EnsureCleanDirectory);
                      });

   Target Compile =>
      _ => _.DependsOn(Restore)
            .Executes(() => DotNetBuild(_ => DefaultDotNetBuild));

   Target Pack =>
      _ => _.DependsOn(Compile)
            .Executes(() =>
                      {
                         // TODO: DeleteDirectory not work, and move to Clean
                         if (Exists(ArtifactsDirectory))
                            Delete(ArtifactsDirectory, true);

                         DotNetPack(_ => DefaultDotNetPack.SetOutputDirectory(ArtifactsDirectory)
                                                          .DisableIncludeSymbols()
                                                          .SetProject("AsyncApostle/AsyncApostle.csproj"));

                         DotNetPack(_ => DefaultDotNetPack.SetOutputDirectory(SolutionDirectory / "Rider" / "AsyncApostle.Rider")
                                                          .DisableIncludeSymbols()
                                                          .SetProject("AsyncApostle/AsyncApostle.Rider.csproj"));

                         CreateFromDirectory(SolutionDirectory / "Rider", ArtifactsDirectory / "AsyncApostle.Rider.zip");
                      });

   Target Restore =>
      _ => _.DependsOn(Clean)
            .Executes(() =>
                      {
                         DotNetRestore(_ => DefaultDotNetRestore.SetProjectFile("AsyncApostle/AsyncApostle.csproj"));
                         DotNetRestore(_ => DefaultDotNetRestore.SetProjectFile("AsyncApostle/AsyncApostle.Rider.csproj"));
                      });

   #endregion

   #region methods

   // Console application entry. Also defines the default target.
   public static int Main() => Execute<Build>(x => x.Compile);

   #endregion
}