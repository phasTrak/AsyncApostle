namespace AsyncApostle.AsyncHelpers.RenameCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
class RenameChecker(IEnumerable<IConcreteRenameChecker> concreteCheckers) : IRenameChecker
{
   readonly IConcreteRenameChecker[] _concreteCheckers = concreteCheckers as IConcreteRenameChecker[] ?? [.. concreteCheckers];
   public bool NeedRename(IMethodDeclaration method) => !Exists(_concreteCheckers, x => x.SkipRename(method));
}