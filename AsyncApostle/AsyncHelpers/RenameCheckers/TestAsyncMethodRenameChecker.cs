using AsyncApostle.Checkers;
using JetBrains.Application.Settings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using static AsyncApostle.Settings.AsyncApostleSettingsAccessor;

namespace AsyncApostle.AsyncHelpers.RenameCheckers
{
    [SolutionComponent]
    class TestRenameChecker : IConcreteRenameChecker
    {
        #region fields

        readonly IUnderTestChecker _underTestChecker;

        #endregion

        #region constructors

        public TestRenameChecker(IUnderTestChecker underTestChecker) => _underTestChecker = underTestChecker;

        #endregion

        #region methods

        public bool SkipRename(IMethodDeclaration methodDeclaration) =>
            methodDeclaration.GetSettingsStore()
                             .GetValue(ExcludeTestMethodsFromRenaming)
            && _underTestChecker.IsUnder(methodDeclaration);

        #endregion
    }
}
