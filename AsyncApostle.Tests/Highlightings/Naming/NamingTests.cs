using AsyncApostle.Tests.Helpers;
using NUnit.Framework;

namespace AsyncApostle.Tests.Highlightings.Naming;

public class NamingTests : HighlightingsTestsBase
{
    protected sealed override string RelativeTestDataPath => @"Highlightings\Naming\Common";

    [TestCaseSource(typeof(TestHelper),
                    nameof(TestHelper.FileNames),
                    new object[]
                    {
                        @"Highlightings\Naming\Common"
                    })]
    public void Test(string fileName) => DoTestSolution(fileName);
}