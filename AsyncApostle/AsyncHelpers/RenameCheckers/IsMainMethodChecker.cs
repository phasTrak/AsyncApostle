namespace AsyncApostle.AsyncHelpers.RenameCheckers;

[SolutionComponent]
public class IsMainMethodChecker : IConcreteRenameChecker
{
   #region methods

   public bool SkipRename(IMethodDeclaration methodDeclaration) =>
      methodDeclaration.DeclaredName is "Main"
   && (methodDeclaration.Type.IsTask()
    || methodDeclaration.Type.IsGenericTask()
    && methodDeclaration.Type.IsGenericTask()
    && methodDeclaration.Type.GetFirstGenericType()
                        .IsInt())
   && (methodDeclaration.ParameterDeclarations.Count is 0
    || methodDeclaration.ParameterDeclarations.Count is 1
    && methodDeclaration.ParameterDeclarations[0]
                        .Type is IArrayType
    && methodDeclaration.ParameterDeclarations[0]
                        .Type.GetScalarType()
                        .IsString());

   #endregion
}