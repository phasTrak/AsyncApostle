namespace AsyncApostle.Checkers;

[SolutionComponent]
public class AttributeTypeChecker : IAttributeTypeChecker
{
   #region methods

   public bool IsUnder(ICSharpTreeNode node)
   {
      var customTypes = node.GetSettingsStore()
                            .EnumIndexedValues(ConfigureAwaitIgnoreAttributeTypes)
                            .ToArray();

      return !customTypes.IsNullOrEmpty()
          && node.GetContainingTypeDeclaration()
                ?.ContainsAttribute(customTypes) is true;
   }

   #endregion
}