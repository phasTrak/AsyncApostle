namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;

[SolutionComponent]
class BaseChecker : IConfigureAwaitCustomChecker
{
   #region methods

   public bool CanBeAdded(IAwaitExpression element)
   {
      var declaredType = element.Task?.GetExpressionType() as IDeclaredType;

      return !declaredType.IsConfigurableAwaitable() && !declaredType.IsGenericConfigurableAwaitable();
   }

   #endregion
}