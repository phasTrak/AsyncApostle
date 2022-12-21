namespace AsyncApostle.AsyncHelpers.RenameCheckers;

public interface IRenameChecker
{
   #region methods

   bool NeedRename(IMethodDeclaration method);

   #endregion
}