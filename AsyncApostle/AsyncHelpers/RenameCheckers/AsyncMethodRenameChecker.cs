namespace AsyncApostle.AsyncHelpers.RenameCheckers;

[SolutionComponent]
class RenameChecker : IRenameChecker
{
   #region fields

   readonly IConcreteRenameChecker[] _concreteCheckers;

   #endregion

   #region constructors

   public RenameChecker(IEnumerable<IConcreteRenameChecker> concreteCheckers) => _concreteCheckers = concreteCheckers.ToArray();

   #endregion

   #region methods

   public bool NeedRename(IMethodDeclaration method) => !_concreteCheckers.Any(x => x.SkipRename(method));

   #endregion
}