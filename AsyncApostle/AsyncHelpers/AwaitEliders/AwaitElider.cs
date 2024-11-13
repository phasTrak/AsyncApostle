namespace AsyncApostle.AsyncHelpers.AwaitEliders;

[SolutionComponent(DemandAnyThreadSafe)]
class AwaitElider(IEnumerable<ICustomAwaitElider> awaitEliders) : IAwaitElider
{
   #region fields

   readonly ICustomAwaitElider[] _awaitEliders = awaitEliders as ICustomAwaitElider[] ?? [..awaitEliders];

   #endregion

   #region methods

   public void Elide(IAwaitExpression awaitExpression)
   {
      if (awaitExpression.Task is not IInvocationExpression invocationExpression) return;

      var declarationOrClosure = awaitExpression.GetContainingFunctionLikeDeclarationOrClosure();

      if (declarationOrClosure is null) return;

      Find(_awaitEliders, x => x.CanElide(declarationOrClosure))
       ?.Elide(declarationOrClosure, invocationExpression.RemoveConfigureAwait());
   }

   public void Elide(IParametersOwnerDeclaration parametersOwnerDeclaration)
   {
      foreach (var awaitExpression in parametersOwnerDeclaration.DescendantsInScope<IAwaitExpression>()) Elide(awaitExpression);
   }

   #endregion
}