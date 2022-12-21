namespace AsyncApostle.AsyncHelpers.AwaitEliders;

[SolutionComponent]
class MethodAwaitElider : ICustomAwaitElider
{
   #region methods

   public bool CanElide(ICSharpDeclaration declarationOrClosure) => declarationOrClosure is IMethodDeclaration;

   public void Elide(ICSharpDeclaration declarationOrClosure, ICSharpExpression awaitExpression)
   {
      if (declarationOrClosure is not IMethodDeclaration methodDeclaration) return;

      methodDeclaration.SetAsync(false);

      if (methodDeclaration.Body is not null)
      {
         awaitExpression.GetContainingStatement()
                       ?.ReplaceBy(GetInstance(awaitExpression)
                                     .CreateStatement("return $0;", awaitExpression));
      }
      else
         methodDeclaration.ArrowClause?.SetExpression(awaitExpression);
   }

   #endregion
}