namespace AsyncApostle.AsyncHelpers.MethodFinders;

[SolutionComponent]
public class MethodFindingChecker(IEnumerable<IConcreteMethodFindingChecker> concreteMethodFindingCheckers) : IMethodFindingChecker
{
   #region fields

   readonly IConcreteMethodFindingChecker[] _concreteMethodFindingCheckers = concreteMethodFindingCheckers as IConcreteMethodFindingChecker[] ?? [..concreteMethodFindingCheckers];

   #endregion

   #region methods

   public bool NeedSkip(IMethod originalMethod, IMethod candidateMethod) => Exists(_concreteMethodFindingCheckers, x => x.NeedSkip(originalMethod, candidateMethod));

   #endregion
}