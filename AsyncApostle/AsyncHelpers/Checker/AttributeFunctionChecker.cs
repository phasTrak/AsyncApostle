namespace AsyncApostle.AsyncHelpers.Checker;

[SolutionComponent(DemandAnyThreadSafe)]
public class AttributeFunctionChecker : IAttributeFunctionChecker
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
          && node.GetContainingFunctionDeclarationIgnoringClosures()
                ?.ContainsAttribute(customTypes) is true;
   }

   #endregion
}