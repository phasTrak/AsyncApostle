namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;

[SolutionComponent]
class BlazorChecker : IConfigureAwaitCustomChecker
{
   #region fields

   const string CodeBehindFileExtension = ".razor.cs";
   const string ComponentAssemblyName   = "Microsoft.AspNetCore.Components";
   const string ComponentBaseclassName  = "Microsoft.AspNetCore.Components.ComponentBase";
   const string FileExtension           = ".razor";

   #endregion

   #region methods

   static IDeclaredType? FindComponentBaseClass(IEnumerable<IDeclaredType> superTypes)
   {
      var declaredTypes = superTypes.ToList();

      foreach (var declaredType in declaredTypes)
      {
         var clrName = declaredType.GetClrName()
                                   .FullName;

         if (string.Equals(clrName, ComponentBaseclassName) && string.Equals(declaredType.Assembly?.Name, ComponentAssemblyName)) return declaredType;

         var componentDeclaration = FindComponentBaseClass(declaredType.GetSuperTypes());

         if (componentDeclaration is not null) return componentDeclaration;
      }

      return null;
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

      if (sourceFile.DisplayName.EndsWith(FileExtension, OrdinalIgnoreCase) || sourceFile.DisplayName.EndsWith(CodeBehindFileExtension, OrdinalIgnoreCase)) return false;

      var classDeclaration = GetClassDeclaration(element);

      if (classDeclaration is null) return true;

      return FindComponentBaseClass(classDeclaration.SuperTypes) is null;
   }

   #endregion
}