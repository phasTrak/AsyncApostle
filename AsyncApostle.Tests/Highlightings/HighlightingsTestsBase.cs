using JetBrains.Application.Settings;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.TestFramework;

namespace AsyncApostle.Tests.Highlightings;

[TestNetCore31]
public abstract class HighlightingsTestsBase : CSharpHighlightingTestBase
{
    protected override void DoTestSolution(params string[] fileSet) =>
        ExecuteWithinSettingsTransaction(settingsStore =>
                                         {
                                             RunGuarded(() => MutateSettings(settingsStore));
                                             base.DoTestSolution(fileSet);
                                         });

    protected virtual void MutateSettings(IContextBoundSettingsStore settingsStore) { }
}