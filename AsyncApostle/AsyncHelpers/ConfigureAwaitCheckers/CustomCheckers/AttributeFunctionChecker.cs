namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
class AttributeFunctionChecker(IAttributeFunctionChecker attributeFunctionChecker) : IConfigureAwaitCustomChecker
{
   #region methods

   public bool CanBeAdded(IAwaitExpression element) => !attributeFunctionChecker.IsUnder(element);

   #endregion
}