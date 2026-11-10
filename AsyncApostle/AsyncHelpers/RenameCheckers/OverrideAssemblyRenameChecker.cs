namespace AsyncApostle.AsyncHelpers.RenameCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
public class OverrideAssemblyRenameChecker : IConcreteRenameChecker
{
   public bool SkipRename(IMethodDeclaration methodDeclaration) =>
      methodDeclaration.DeclaredElement?.FindBaseMethods()
                       .Any(static baseMethod => baseMethod.GetSourceFiles()
                                                           .IsEmpty) is true;
}