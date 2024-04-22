namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;

[SolutionComponent]
class BlazorChecker(IAttributeFunctionChecker attributeFunctionChecker) : IConfigureAwaitCustomChecker
{
   #region Constants

   const string FILE_EXTENSION             = ".razor";
   const string CODE_BEHIND_FILE_EXTENSION = ".razor.cs";
   const string COMPONENT_BASECLASS_NAME   = "Microsoft.AspNetCore.Components.ComponentBase";
   const string COMPONENT_ASSEMBLY_NAME    = "Microsoft.AspNetCore.Components";

   #endregion

   #region methods

   public bool CanBeAdded(IAwaitExpression element)
   {
      var sourceFile = element.GetSourceFile();

      if (sourceFile is null) return true;

      if (sourceFile.DisplayName.EndsWith(FILE_EXTENSION, OrdinalIgnoreCase)
       || sourceFile.DisplayName.EndsWith(CODE_BEHIND_FILE_EXTENSION, OrdinalIgnoreCase))
         return false;

      var classDeclaration = GetClassDeclaration(element);

      if (classDeclaration is null) return true;

      return FindComponentBaseClass(classDeclaration.SuperTypes) is null;
   }

   static IClassDeclaration? GetClassDeclaration(ITreeNode? node) =>
      node switch
      {
         null                               => null,
         IClassDeclaration classDeclaration => classDeclaration,
         _                                  => GetClassDeclaration(node.Parent)
      };

   static IDeclaredType? FindComponentBaseClass(IEnumerable<IDeclaredType> superTypes)
   {
      var declaredTypes = superTypes.ToList();

      foreach (var declaredType in declaredTypes)
      {
         var clrName = declaredType.GetClrName()
                                   .FullName;

         if (String.Equals(clrName, COMPONENT_BASECLASS_NAME)
          && String.Equals(declaredType.Assembly?.Name, COMPONENT_ASSEMBLY_NAME))
            return declaredType;

         var componentDeclaration = FindComponentBaseClass(declaredType.GetSuperTypes());

         if (componentDeclaration is not null) return componentDeclaration;
      }

      return null;
   }

   #endregion
}
