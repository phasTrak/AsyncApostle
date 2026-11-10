namespace AsyncApostle.AsyncHelpers.RenameCheckers;

public interface IRenameChecker
{
   bool NeedRename(IMethodDeclaration method);
}