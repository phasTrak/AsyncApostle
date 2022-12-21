namespace AsyncApostle.AsyncHelpers.Checker;

[SolutionComponent]
public class AttributeFunctionChecker : IAttributeFunctionChecker
{
   #region methods

   public bool IsUnder(ICSharpTreeNode node)
   {
      var customTypes = node.GetSettingsStore()
                            .EnumIndexedValues(ConfigureAwaitIgnoreAttributeTypes)
                            .ToArray();

      return !customTypes.IsNullOrEmpty()
          && node.GetContainingFunctionDeclarationIgnoringClosures()
                ?.ContainsAttribute(customTypes) is true;
   }

   #endregion
}