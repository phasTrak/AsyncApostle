using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.AsyncHelpers.RenameCheckers;

public interface IRenameChecker
{
   #region methods

   bool NeedRename(IMethodDeclaration method);

   #endregion
}