namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
class CanBeUseAsyncMethodChecker(IEnumerable<IConcreteCanBeUseAsyncMethodChecker> checkers) : ICanBeUseAsyncMethodChecker
{
   readonly IConcreteCanBeUseAsyncMethodChecker[] _checkers = checkers as IConcreteCanBeUseAsyncMethodChecker[] ?? [.. checkers];
   public bool CanReplace(IInvocationExpression element) => TrueForAll(_checkers, x => x.CanReplace(element));
}