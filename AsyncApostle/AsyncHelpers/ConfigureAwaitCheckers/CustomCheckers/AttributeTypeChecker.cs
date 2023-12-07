namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;

[SolutionComponent]
class AttributeTypeChecker(IAttributeTypeChecker attributeTypeChecker) : IConfigureAwaitCustomChecker
{
   #region methods

   public bool CanBeAdded(IAwaitExpression element) => !attributeTypeChecker.IsUnder(element);

   #endregion
}