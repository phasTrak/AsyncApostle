﻿using AsyncApostle.QuickFixes;
using AsyncApostle.Tests.Helpers;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;

namespace AsyncApostle.Tests.QuickFixes
{
    [TestNetFramework46]
    public class NamingTests : CSharpQuickFixTestBase<AsyncMethodNamingQuickFix>
    {
        [TestCaseSource(typeof(TestHelper),
                        nameof(TestHelper.FileNames),
                        new object[]
                        {
                            @"QuickFixes\" + nameof(NamingTests)
                        })]
        public void Test(string fileName) => DoTestSolution(fileName);
    }
}
