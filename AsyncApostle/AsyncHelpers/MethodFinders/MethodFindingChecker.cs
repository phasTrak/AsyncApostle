namespace AsyncApostle.AsyncHelpers.MethodFinders;

[SolutionComponent]
public class MethodFindingChecker : IMethodFindingChecker
{
   #region fields

   readonly IConcreteMethodFindingChecker[] _concreteMethodFindingCheckers;

   #endregion

   #region constructors

   public MethodFindingChecker(IEnumerable<IConcreteMethodFindingChecker> concreteMethodFindingCheckers) => _concreteMethodFindingCheckers = concreteMethodFindingCheckers.ToArray();

   #endregion

   #region methods

   public bool NeedSkip(IMethod originalMethod, IMethod candidateMethod) => _concreteMethodFindingCheckers.Any(x => x.NeedSkip(originalMethod, candidateMethod));

   #endregion
}