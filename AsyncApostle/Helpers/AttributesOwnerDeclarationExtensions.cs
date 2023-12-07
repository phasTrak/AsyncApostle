namespace AsyncApostle.Helpers;

public static class AttributesOwnerDeclarationExtensions
{
   #region methods

   public static bool ContainsAttribute(this IAttributesOwnerDeclaration declaration, IEnumerable<ClrTypeName> attributeNames)
   {
      HashSet<ClrTypeName> clrTypeNames = [..attributeNames];

      return !clrTypeNames.IsNullOrEmpty()
          && declaration.AttributesEnumerable.Select(static attribute => attribute.Name.Reference.Resolve()
                                                                                  .DeclaredElement)
                        .OfType<IClass>()
                        .Select(static attributeClass => attributeClass.GetClrName())
                        .Any(clrTypeNames.Contains);
   }

   public static bool ContainsAttribute(this IAttributesOwnerDeclaration declaration, IEnumerable<string> attributeNames)
   {
      HashSet<ClrTypeName> clrTypeNames = [..attributeNames.Select(NewClrTypeName)];

      return !clrTypeNames.IsNullOrEmpty()
          && declaration.AttributesEnumerable.Select(static attribute => attribute.Name.Reference.Resolve()
                                                                                  .DeclaredElement)
                        .OfType<IClass>()
                        .Select(static attributeClass => attributeClass.GetClrName())
                        .Any(clrTypeNames.Contains);

      static ClrTypeName NewClrTypeName(string name) => new (name);
   }

   #endregion
}