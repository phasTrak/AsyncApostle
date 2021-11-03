using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using static System.StringComparison;

namespace AsyncApostle.AsyncHelpers.RenameCheckers
{
    [SolutionComponent]
    class DefaultRenameChecker : IConcreteRenameChecker
    {
        #region methods

        public bool SkipRename(IMethodDeclaration methodDeclaration) => !methodDeclaration.Type.IsTask() && !methodDeclaration.Type.IsGenericTask() || methodDeclaration.DeclaredName.EndsWith("Async", Ordinal);

        #endregion
    }
}
