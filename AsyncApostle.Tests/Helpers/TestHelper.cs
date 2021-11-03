using System.Linq;
using NUnit.Framework;
using static System.IO.Directory;
using static System.IO.Path;
using static NUnit.Framework.TestContext;

namespace AsyncApostle.Tests.Helpers
{
    public class TestHelper
    {
        #region methods

        public static TestCaseData[] FileNames(string folder) =>
            GetFiles(Combine(new[]
                             {
                                 CurrentContext.TestDirectory,
                                 @"..\..\..\..\Test\Data",
                                 folder
                             }.ToArray()),
                     "*.cs")
                .Select(x => new TestCaseData(GetFileName(x)))
                .ToArray();

        #endregion
    }
}
