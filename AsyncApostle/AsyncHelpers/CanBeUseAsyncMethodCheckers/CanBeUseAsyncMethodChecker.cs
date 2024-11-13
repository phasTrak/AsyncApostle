namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
class CanBeUseAsyncMethodChecker(IEnumerable<IConcreteCanBeUseAsyncMethodChecker> checkers) : ICanBeUseAsyncMethodChecker
{
   #region fields

   readonly IConcreteCanBeUseAsyncMethodChecker[] _checkers = checkers as IConcreteCanBeUseAsyncMethodChecker[] ?? [..checkers];

   #endregion

   #region methods

   public bool CanReplace(IInvocationExpression element) => TrueForAll(_checkers, x => x.CanReplace(element));

   #endregion
}