namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
class AttributeFunctionChecker(IAttributeFunctionChecker attributeFunctionChecker) : IConfigureAwaitCustomChecker
{
   public bool CanBeAdded(IAwaitExpression element) => !attributeFunctionChecker.IsUnder(element);
}