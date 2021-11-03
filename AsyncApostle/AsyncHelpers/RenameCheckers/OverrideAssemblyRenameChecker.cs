using System.Linq;
using AsyncApostle.Helpers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.AsyncHelpers.RenameCheckers
{
    [SolutionComponent]
    public class OverrideAssemblyRenameChecker : IConcreteRenameChecker
    {
        #region methods

        public bool SkipRename(IMethodDeclaration methodDeclaration) =>
            methodDeclaration.DeclaredElement?.FindBaseMethods()
                             .Any(baseMethod => baseMethod.GetSourceFiles()
                                                          .IsEmpty) is true;

        #endregion
    }
}
