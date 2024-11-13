namespace AsyncApostle.AsyncHelpers.AwaitEliders;

[SolutionComponent(DemandAnyThreadSafe)]
class LambdaAwaitElider : ICustomAwaitElider
{
   #region methods

   public bool CanElide(ICSharpDeclaration declarationOrClosure) => declarationOrClosure is ILambdaExpression;

   public void Elide(ICSharpDeclaration declarationOrClosure, ICSharpExpression awaitExpression)
   {
      if (declarationOrClosure is not ILambdaExpression lambdaExpression) return;

      lambdaExpression.SetAsync(false);

      if (lambdaExpression.BodyBlock is not null)
      {
         awaitExpression.GetContainingStatement()
                       ?.ReplaceBy(GetInstance(awaitExpression)
                                     .CreateStatement("return $0;", awaitExpression));
      }
      else
         lambdaExpression.SetBodyExpression(awaitExpression);
   }

   #endregion
}