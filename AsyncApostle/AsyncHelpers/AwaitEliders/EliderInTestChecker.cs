using AsyncApostle.Checkers;
using AsyncApostle.Helpers;
using JetBrains.Application.Settings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using static AsyncApostle.Settings.AsyncApostleSettingsAccessor;

namespace AsyncApostle.AsyncHelpers.AwaitEliders
{
    [SolutionComponent]
    public class EliderInTestChecker : IConcreteAwaitEliderChecker
    {
        #region fields

        readonly IUnderTestChecker _underTestChecker;

        #endregion

        #region constructors

        public EliderInTestChecker(IUnderTestChecker underTestChecker) => _underTestChecker = underTestChecker;

        #endregion

        #region methods

        public bool CanElide(IParametersOwnerDeclaration element) =>
            !element.GetSettingsStore()
                    .GetValue(ExcludeTestMethodsFromEliding)
            || element is not IMethodDeclaration method
            || !_underTestChecker.IsUnder(method);

        #endregion
    }
}
