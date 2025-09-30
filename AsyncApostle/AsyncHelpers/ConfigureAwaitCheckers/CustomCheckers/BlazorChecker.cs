namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
class BlazorChecker : IConfigureAwaitCustomChecker
{
   #region fields

   const string CODE_BEHIND_FILE_EXTENSION = ".razor.cs";
   const string COMPONENT_BASE_NAME        = "Microsoft.AspNetCore.Components.IComponent";
   const string FILE_EXTENSION             = ".razor";

   #endregion

   #region methods

   static IDeclaredType? FindComponentBaseClass(IEnumerable<IDeclaredType> superTypes)
   {
      var declaredTypes = superTypes.ToList();
      var visited = new HashSet<IDeclaredType>();

      return FindComponentBaseClassInternal(declaredTypes);

      IDeclaredType? FindComponentBaseClassInternal(IEnumerable<IDeclaredType> types)
      {
         foreach (var declaredType in types)
         {
            if (!visited.Add(declaredType))
               continue;

            if (declaredType.GetSuperTypes()
                            .Any(static i => i.GetClrName()
                                              .FullName.Equals(COMPONENT_BASE_NAME)))
               return declaredType;

            var componentDeclaration = FindComponentBaseClassInternal(declaredType.GetSuperTypes());

            if (componentDeclaration is not null) return componentDeclaration;
         }

         return null;
      }
   }

   static IClassDeclaration? GetClassDeclaration(ITreeNode? node) =>
      node switch
      {
         null                               => null,
         IClassDeclaration classDeclaration => classDeclaration,
         _                                  => GetClassDeclaration(node.Parent)
      };

   public bool CanBeAdded(IAwaitExpression element)
   {
      var sourceFile = element.GetSourceFile();

      if (sourceFile is null) return true;

      if (sourceFile.DisplayName.EndsWith(FILE_EXTENSION, OrdinalIgnoreCase) || sourceFile.DisplayName.EndsWith(CODE_BEHIND_FILE_EXTENSION, OrdinalIgnoreCase)) return false;

      var classDeclaration = GetClassDeclaration(element);

      if (classDeclaration is null) return true;

      return FindComponentBaseClass(classDeclaration.SuperTypes) is null;
   }

   #endregion
}