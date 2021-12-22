using System.IO;
using Nuke.Common.Tools.DotNet;
using Nuke.Core;
using Nuke.Core.Utilities.Collections;
using static System.Console;
using static System.Environment;
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
   string Version => "2021.3.1.1";

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
                         {
                            WriteLine($@"Delete ""{ArtifactsDirectory}""{NewLine}");
                            Delete(ArtifactsDirectory, true);
                         }

                         DotNetPack(_ => DefaultDotNetPack.SetOutputDirectory(ArtifactsDirectory)
                                                          .DisableIncludeSymbols()
                                                          .SetProject("AsyncApostle/AsyncApostle.csproj"));

                         DotNetPack(_ => DefaultDotNetPack.SetOutputDirectory(SolutionDirectory / "Rider" / "AsyncApostle.Rider")
                                                          .DisableIncludeSymbols()
                                                          .SetProject("AsyncApostle/AsyncApostle.Rider.csproj"));

                         WriteLine($@"Unzip ""{SolutionDirectory / "Rider" / "AsyncApostle.Rider" / $"AsyncApostle.Rider.{Version}.nupkg"}"" to ""{ArtifactsDirectory / $"AsyncApostle.Rider.{Version}"}""{NewLine}");
                         ExtractToDirectory(SolutionDirectory / "Rider" / "AsyncApostle.Rider" / $"AsyncApostle.Rider.{Version}.nupkg", ArtifactsDirectory / $"AsyncApostle.Rider.{Version}");

                         WriteLine($@"Delete {SolutionDirectory / "Rider" / "AsyncApostle.Rider" / $"AsyncApostle.Rider.{Version}.nupkg"}{NewLine}");
                         File.Delete(SolutionDirectory / "Rider" / "AsyncApostle.Rider" / $"AsyncApostle.Rider.{Version}.nupkg");

                         WriteLine($@"Create ""{ArtifactsDirectory / "Rider"}""{NewLine}");
                         CreateDirectory(ArtifactsDirectory / "Rider");

                         WriteLine($@"Move ""{ArtifactsDirectory / $"AsyncApostle.Rider.{Version}" / "lib"}"" to ""{ArtifactsDirectory / "Rider" / "AsyncApostle.Rider"}""{NewLine}");
                         Move(ArtifactsDirectory / $"AsyncApostle.Rider.{Version}" / "lib", ArtifactsDirectory / "Rider" / "AsyncApostle.Rider");

                         WriteLine($@"Delete ""{ArtifactsDirectory / $"AsyncApostle.Rider.{Version}"}""{NewLine}");
                         Delete(ArtifactsDirectory / $"AsyncApostle.Rider.{Version}", true);

                         WriteLine($@"Create ""{ArtifactsDirectory / "Rider" / "AsyncApostle.Rider" / "lib"}""{NewLine}");
                         CreateDirectory(ArtifactsDirectory / "Rider" / "AsyncApostle.Rider" / "lib");

                         WriteLine($@"Zip ""{SolutionDirectory / "Rider" / "AsyncApostle.Rider"}"" into ""{ArtifactsDirectory / "Rider" / "AsyncApostle.Rider" / "lib" / $"AsyncApostle.Rider-{Version}.jar"}""{NewLine}");
                         CreateFromDirectory(SolutionDirectory / "Rider" / "AsyncApostle.Rider", ArtifactsDirectory / "Rider" / "AsyncApostle.Rider" / "lib" / $"AsyncApostle.Rider-{Version}.jar");

                         WriteLine($@"Zip ""{ArtifactsDirectory / "Rider"}"" into ""{ArtifactsDirectory / "AsyncApostle.Rider.zip"}""{NewLine}");
                         CreateFromDirectory(ArtifactsDirectory / "Rider", ArtifactsDirectory / "AsyncApostle.Rider.zip");

                         WriteLine($@"Delete ""{ArtifactsDirectory / "Rider"}""{NewLine}");
                         Delete(ArtifactsDirectory / "Rider", true);
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