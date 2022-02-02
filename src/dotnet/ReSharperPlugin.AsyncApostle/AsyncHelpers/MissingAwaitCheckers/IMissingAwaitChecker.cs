using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.AsyncHelpers.MissingAwaitChecker;

public interface IMissingAwaitChecker
{
   #region methods

   bool AwaitIsMissing(IParametersOwnerDeclaration element);

   #endregion
}