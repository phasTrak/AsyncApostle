namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
class InAsyncMethodChecker : IConcreteCanBeUseAsyncMethodChecker
{
   #region methods

   public bool CanReplace(IInvocationExpression element) => element.IsUnderAsyncDeclaration();

   #endregion
}