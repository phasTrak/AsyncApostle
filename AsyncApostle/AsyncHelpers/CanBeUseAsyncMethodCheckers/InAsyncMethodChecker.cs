namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers;

[SolutionComponent]
class InAsyncMethodChecker : IConcreteCanBeUseAsyncMethodChecker
{
   #region methods

   public bool CanReplace(IInvocationExpression element) => element.IsUnderAsyncDeclaration();

   #endregion
}