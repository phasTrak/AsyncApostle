namespace AsyncApostle.AsyncHelpers.RenameCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
class DefaultRenameChecker : IConcreteRenameChecker
{
   public bool SkipRename(IMethodDeclaration methodDeclaration) => !methodDeclaration.Type.IsTask() && !methodDeclaration.Type.IsGenericTask() || methodDeclaration.DeclaredName.EndsWith("Async", Ordinal);
}