namespace AsyncApostle.AsyncHelpers.RenameCheckers;

public interface IConcreteRenameChecker
{
   #region methods

   bool SkipRename(IMethodDeclaration methodDeclaration);

   #endregion
}