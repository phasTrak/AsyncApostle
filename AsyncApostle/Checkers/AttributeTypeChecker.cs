namespace AsyncApostle.Checkers;

[SolutionComponent]
public class AttributeTypeChecker : IAttributeTypeChecker
{
   #region methods

   public bool IsUnder(ICSharpTreeNode node)
   {
      string[] customTypes =
      [
         ..node.GetSettingsStore()
               .EnumIndexedValues(ConfigureAwaitIgnoreAttributeTypes)
      ];

      return !customTypes.IsNullOrEmpty()
          && node.GetContainingTypeDeclaration()
                ?.ContainsAttribute(customTypes) is true;
   }

   #endregion
}