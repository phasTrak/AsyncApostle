namespace AsyncApostle.AsyncHelpers.RenameCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
public class OverrideAssemblyRenameChecker : IConcreteRenameChecker
{
   #region methods

   public bool SkipRename(IMethodDeclaration methodDeclaration) =>
      methodDeclaration.DeclaredElement?.FindBaseMethods()
                       .Any(static baseMethod => baseMethod.GetSourceFiles()
                                                           .IsEmpty) is true;

   #endregion
}