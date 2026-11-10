namespace AsyncApostle.AsyncHelpers.Checker;

[SolutionComponent(DemandAnyThreadSafe)]
public class AttributeFunctionChecker : IAttributeFunctionChecker
{
   public bool IsUnder(ICSharpTreeNode node)
   {
      string[] customTypes =
      [
         .. node.GetSettingsStore()
                .EnumIndexedValues(ConfigureAwaitIgnoreAttributeTypes)
      ];

      return !customTypes.IsNullOrEmpty()
          && node.GetContainingFunctionDeclarationIgnoringClosures()
                ?.ContainsAttribute(customTypes) is true;
   }
}