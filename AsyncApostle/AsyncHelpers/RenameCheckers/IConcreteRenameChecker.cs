namespace AsyncApostle.AsyncHelpers.RenameCheckers;

public interface IConcreteRenameChecker
{
   bool SkipRename(IMethodDeclaration methodDeclaration);
}