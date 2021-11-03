using AsyncApostle.Tests.Helpers;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;

namespace AsyncApostle.Tests.Highlightings.Naming
{
    [TestReferences("LibraryToOverride.dll")]
    public class NamingWithExternalAssemblyTests : HighlightingsTestsBase
    {
        protected sealed override string RelativeTestDataPath => @"Highlightings\Naming\WithOverride";

        [TestCaseSource(typeof(TestHelper),
                        nameof(TestHelper.FileNames),
                        new object[]
                        {
                            @"Highlightings\Naming\WithOverride"
                        })]
        public void Test(string fileName) => DoTestSolution(fileName);
    }
}
