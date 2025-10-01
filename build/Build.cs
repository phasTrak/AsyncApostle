using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static System.Console;
using static System.Environment;
using static System.IO.Compression.ZipFile;
using static System.IO.Directory;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;

class Build : NukeBuild
{
   #region properties

   public override AbsolutePath ArtifactsDirectory => SolutionDirectory / "packages";

   static          string       Version            => "2025.2.1-beta01";

   Target Clean =>
      d => d.Executes(() =>
                      {
                         EnsureCleanDirectory(ArtifactsDirectory);

                         GlobDirectories(SolutionDirectory / "AsyncApostle", "**/bin", "**/obj")
                           .ForEach(EnsureCleanDirectory);
                      });

   Target Compile =>
      d => d.DependsOn(Restore)
            .Executes(static () => DotNetBuild(static _ => DefaultDotNetBuild));

   Target Pack =>
      d => d.DependsOn(Compile)
            .Executes(() =>
                      {
                         // TODO: DeleteDirectory not work, and move to Clean
                         if (Exists(ArtifactsDirectory))
                         {
                            WriteLine($"""Delete "{ArtifactsDirectory}"{NewLine}""");
                            Delete(ArtifactsDirectory, true);
                         }

                         DotNetPack(_ => DefaultDotNetPack.SetOutputDirectory(ArtifactsDirectory)
                                                          .DisableIncludeSymbols()
                                                          .SetProject("AsyncApostle/AsyncApostle.csproj"));

                         DotNetPack(_ => DefaultDotNetPack.SetOutputDirectory(SolutionDirectory / "Rider" / "AsyncApostle.Rider")
                                                          .DisableIncludeSymbols()
                                                          .SetProject("AsyncApostle/AsyncApostle.Rider.csproj"));

                         WriteLine($"""Unzip "{SolutionDirectory / "Rider" / "AsyncApostle.Rider" / $"AsyncApostle.Rider.{Version}.nupkg"}" to "{ArtifactsDirectory / $"AsyncApostle.Rider.{Version}"}"{NewLine}""");
                         ExtractToDirectory(SolutionDirectory / "Rider" / "AsyncApostle.Rider" / $"AsyncApostle.Rider.{Version}.nupkg", ArtifactsDirectory / $"AsyncApostle.Rider.{Version}");

                         WriteLine($"Delete {SolutionDirectory / "Rider" / "AsyncApostle.Rider" / $"AsyncApostle.Rider.{Version}.nupkg"}{NewLine}");
                         File.Delete(SolutionDirectory / "Rider" / "AsyncApostle.Rider" / $"AsyncApostle.Rider.{Version}.nupkg");

                         WriteLine($"""Create "{ArtifactsDirectory / "Rider"}"{NewLine}""");
                         CreateDirectory(ArtifactsDirectory / "Rider");

                         WriteLine($"""Move "{ArtifactsDirectory / $"AsyncApostle.Rider.{Version}" / "lib"}" to "{ArtifactsDirectory / "Rider" / "AsyncApostle.Rider"}"{NewLine}""");
                         Move(ArtifactsDirectory / $"AsyncApostle.Rider.{Version}" / "lib", ArtifactsDirectory / "Rider" / "AsyncApostle.Rider");

                         WriteLine($"""Rename "{ArtifactsDirectory / "Rider" / "AsyncApostle.Rider" / "net472"}" to "{ArtifactsDirectory / "Rider" / "AsyncApostle.Rider" / "dotnet"}"{NewLine}""");
                         Move(ArtifactsDirectory / "Rider" / "AsyncApostle.Rider" / "net472", ArtifactsDirectory / "Rider" / "AsyncApostle.Rider" / "dotnet");

                         WriteLine($"""Delete "{ArtifactsDirectory / $"AsyncApostle.Rider.{Version}"}"{NewLine}""");
                         Delete(ArtifactsDirectory / $"AsyncApostle.Rider.{Version}", true);

                         WriteLine($"""Create "{ArtifactsDirectory / "Rider" / "AsyncApostle.Rider" / "lib"}"{NewLine}""");
                         CreateDirectory(ArtifactsDirectory / "Rider" / "AsyncApostle.Rider" / "lib");

                         WriteLine($"""Zip "{SolutionDirectory / "Rider" / "AsyncApostle.Rider"}" into "{ArtifactsDirectory / "Rider" / "AsyncApostle.Rider" / "lib" / $"AsyncApostle.Rider-{Version}.jar"}"{NewLine}""");
                         CreateFromDirectory(SolutionDirectory / "Rider" / "AsyncApostle.Rider", ArtifactsDirectory / "Rider" / "AsyncApostle.Rider" / "lib" / $"AsyncApostle.Rider-{Version}.jar");

                         WriteLine($"""Zip "{ArtifactsDirectory / "Rider"}" into "{ArtifactsDirectory / "AsyncApostle.Rider.zip"}"{NewLine}""");
                         CreateFromDirectory(ArtifactsDirectory / "Rider", ArtifactsDirectory / "AsyncApostle.Rider.zip");

                         WriteLine($"""Delete "{ArtifactsDirectory / "Rider"}"{NewLine}""");
                         Delete(ArtifactsDirectory / "Rider", true);
                      });

   Target Restore =>
      d => d.DependsOn(Clean)
            .Executes(static () =>
                      {
                         DotNetRestore(static _ => DefaultDotNetRestore.SetProjectFile("AsyncApostle/AsyncApostle.csproj"));
                         DotNetRestore(static _ => DefaultDotNetRestore.SetProjectFile("AsyncApostle/AsyncApostle.Rider.csproj"));
                      });

   #endregion

   #region methods

   // Console application entry. Also defines the default target.
   public static int Main() => Execute<Build>(static x => x.Compile);

   #endregion
}