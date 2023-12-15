namespace AsyncApostle.AsyncHelpers.RenameCheckers;

[SolutionComponent]
class ControllerRenameChecker : IConcreteRenameChecker
{
   #region fields

   readonly HashSet<ClrTypeName> _controllerClasses =
   [
      new ("System.Web.Mvc.Controller"),
      new ("System.Web.Http.ApiController"),
      new ("Microsoft.AspNetCore.Mvc.Controller"),
      new ("Microsoft.AspNetCore.Mvc.ControllerBase")
   ];

   #endregion

   #region methods

   public bool SkipRename(IMethodDeclaration methodDeclaration) =>
      methodDeclaration.DeclaredElement?.GetContainingType() is IClass @class && Exists(@class.GetSuperTypesWithoutCircularDependent(), superType => _controllerClasses.Contains(superType.GetClrName()));

   #endregion
}