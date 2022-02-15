using System.Collections.Generic;
using System.Linq;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.Util;

namespace AsyncApostle.Helpers;

public static class AttributesOwnerDeclarationExtensions
{
   #region methods

   public static bool ContainsAttribute(this IAttributesOwnerDeclaration declaration, IEnumerable<ClrTypeName> attributeNames)
   {
      var clrTypeNames = new HashSet<ClrTypeName>(attributeNames);

      return !clrTypeNames.IsNullOrEmpty()
          && declaration.AttributesEnumerable.Select(attribute => attribute.Name.Reference.Resolve()
                                                                           .DeclaredElement)
                        .OfType<IClass>()
                        .Select(attributeClass => attributeClass.GetClrName())
                        .Any(clrTypeNames.Contains);
   }

   public static bool ContainsAttribute(this IAttributesOwnerDeclaration declaration, IEnumerable<string> attributeNames)
   {
      static ClrTypeName NewClrTypeName(string name) => new (name);

      var clrTypeNames = new HashSet<ClrTypeName>(attributeNames.Select(NewClrTypeName));

      return !clrTypeNames.IsNullOrEmpty()
          && declaration.AttributesEnumerable.Select(attribute => attribute.Name.Reference.Resolve()
                                                                           .DeclaredElement)
                        .OfType<IClass>()
                        .Select(attributeClass => attributeClass.GetClrName())
                        .Any(clrTypeNames.Contains);
   }

   #endregion
}