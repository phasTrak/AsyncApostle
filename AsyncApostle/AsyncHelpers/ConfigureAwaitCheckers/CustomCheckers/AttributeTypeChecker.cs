namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;

[SolutionComponent]
class AttributeTypeChecker : IConfigureAwaitCustomChecker
{
   #region fields

   readonly IAttributeTypeChecker _attributeTypeChecker;

   #endregion

   #region constructors

   public AttributeTypeChecker(IAttributeTypeChecker attributeTypeChecker) => _attributeTypeChecker = attributeTypeChecker;

   #endregion

   #region methods

   public bool CanBeAdded(IAwaitExpression element) => !_attributeTypeChecker.IsUnder(element);

   #endregion
}