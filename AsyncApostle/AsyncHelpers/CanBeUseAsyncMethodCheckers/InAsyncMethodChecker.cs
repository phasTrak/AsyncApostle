namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
class InAsyncMethodChecker : IConcreteCanBeUseAsyncMethodChecker
{
   public bool CanReplace(IInvocationExpression element) => element.IsUnderAsyncDeclaration();
}