namespace AsyncApostle.AsyncHelpers.RenameCheckers;

[SolutionComponent]
class RenameChecker(IEnumerable<IConcreteRenameChecker> concreteCheckers) : IRenameChecker
{
   #region fields

   readonly IConcreteRenameChecker[] _concreteCheckers = concreteCheckers as IConcreteRenameChecker[] ?? [..concreteCheckers];

   #endregion

   #region methods

   public bool NeedRename(IMethodDeclaration method) => !Exists(_concreteCheckers, x => x.SkipRename(method));

   #endregion
}