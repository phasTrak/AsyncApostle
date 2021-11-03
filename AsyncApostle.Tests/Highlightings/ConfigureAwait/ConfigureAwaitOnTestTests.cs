using AsyncApostle.Settings.ConfigureAwaitOptions;
using AsyncApostle.Tests.Helpers;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;

namespace AsyncApostle.Tests.Highlightings.ConfigureAwait
{
    [TestSetting(typeof(AsyncApostleConfigureAwaitSettings), nameof(AsyncApostleConfigureAwaitSettings.ExcludeTestMethodsFromConfigureAwait), false)]
    public class ConfigureAwaitOnTestTests : HighlightingsTestsBase
    {
        protected sealed override string RelativeTestDataPath => @"Highlightings\ConfigureAwait\OnTest";

        [TestCaseSource(typeof(TestHelper),
                        nameof(TestHelper.FileNames),
                        new object[]
                        {
                            @"Highlightings\ConfigureAwait\OnTest"
                        })]
        public void Test(string fileName) => DoTestSolution(fileName);
    }
}
