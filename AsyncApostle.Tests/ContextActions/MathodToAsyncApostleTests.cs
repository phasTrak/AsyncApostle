using AsyncApostle.ContextActions;
using AsyncApostle.Tests.Helpers;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;
using static System.String;

namespace AsyncApostle.Tests.ContextActions;

[TestNetFramework46]
public class MethodToAsyncApostleTests : CSharpContextActionExecuteTestBase<MethodToAsyncApostle>
{
    protected override string ExtraPath => Empty;
    protected override string RelativeTestDataPath => $@"ContextActions\{nameof(MethodToAsyncApostleTests)}";

    [TestCaseSource(typeof(TestHelper),
                    nameof(TestHelper.FileNames),
                    new object[]
                    {
                        $@"ContextActions\{nameof(MethodToAsyncApostleTests)}"
                    })]
    public void Test(string fileName) => DoTestSolution(fileName);
}