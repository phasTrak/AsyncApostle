using AsyncApostle.Tests.Helpers;
using NUnit.Framework;

namespace AsyncApostle.Tests.Highlightings.ConfigureAwait;

public class ConfigureAwaitTests : HighlightingsTestsBase
{
    protected sealed override string RelativeTestDataPath => @"Highlightings\ConfigureAwait\Common";

    [TestCaseSource(typeof(TestHelper),
                    nameof(TestHelper.FileNames),
                    new object[]
                    {
                        @"Highlightings\ConfigureAwait\Common"
                    })]
    public void Test(string fileName) => DoTestSolution(fileName);
}