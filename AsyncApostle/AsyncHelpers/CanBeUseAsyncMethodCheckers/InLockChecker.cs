namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
class InLockChecker : IConcreteCanBeUseAsyncMethodChecker
{
   public bool CanReplace(IInvocationExpression element) => element.GetContainingNode<ILockStatement>() is null;
}