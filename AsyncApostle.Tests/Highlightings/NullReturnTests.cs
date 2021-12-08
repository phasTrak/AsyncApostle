using AsyncApostle.Tests.Helpers;
using NUnit.Framework;

namespace AsyncApostle.Tests.Highlightings;

public class NullReturnTests : HighlightingsTestsBase
{
   [TestCaseSource(typeof(TestHelper), nameof(TestHelper.FileNames), new object[] { $@"Highlightings\{nameof(NullReturnTests)}" })]
   public void Test(string fileName) => DoTestSolution(fileName);
}