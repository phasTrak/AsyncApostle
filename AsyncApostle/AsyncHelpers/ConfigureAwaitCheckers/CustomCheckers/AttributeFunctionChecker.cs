namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;

[SolutionComponent]
class AttributeFunctionChecker(IAttributeFunctionChecker attributeFunctionChecker) : IConfigureAwaitCustomChecker
{
   #region methods

   public bool CanBeAdded(IAwaitExpression element) => !attributeFunctionChecker.IsUnder(element);

   #endregion
}