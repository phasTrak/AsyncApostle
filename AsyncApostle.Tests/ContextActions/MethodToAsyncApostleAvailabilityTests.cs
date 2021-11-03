using AsyncApostle.ContextActions;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;
using static System.String;

namespace AsyncApostle.Tests.ContextActions
{
    [TestNetFramework45]
    public class MethodToAsyncApostleAvailabilityTests : CSharpContextActionAvailabilityTestBase<MethodToAsyncApostle>
    {
        protected override string ExtraPath => Empty;
        protected override string RelativeTestDataPath => @"ContextActions\MethodToAsyncApostleAvailabilityTests";

        [Test]
        public void Test() => DoTestSolution("Test01.cs");
    }
}
