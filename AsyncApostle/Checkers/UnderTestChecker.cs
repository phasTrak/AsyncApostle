using System.Collections.Generic;
using AsyncApostle.Helpers;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.Checkers;

[SolutionComponent]
public class UnderTestChecker : IUnderTestChecker
{
    #region fields

    readonly HashSet<ClrTypeName> _testAttributesClass = new ()
                                                         {
                                                             new ("Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute"),
                                                             new ("Microsoft.VisualStudio.TestTools.UnitTesting.DataTestMethodAttribute"),
                                                             new ("Xunit.FactAttribute"),
                                                             new ("Xunit.TheoryAttribute"),
                                                             new ("NUnit.Framework.TestAttribute"),
                                                             new ("NUnit.Framework.TestCaseAttribute"),
                                                             new ("NUnit.Framework.TestCaseSourceAttribute")
                                                         };

    #endregion

    #region methods

    public bool IsUnder(IMethodDeclaration method) => method.AttributeSectionList is not null && method.ContainsAttribute(_testAttributesClass);

    #endregion
}