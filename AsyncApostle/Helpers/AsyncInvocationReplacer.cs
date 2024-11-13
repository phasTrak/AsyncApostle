namespace AsyncApostle.Helpers;

[SolutionComponent(DemandAnyThreadSafe)]
class AsyncInvocationReplacer : IAsyncInvocationReplacer
{
   #region methods

   public void ReplaceInvocation(IInvocationExpression? invocation, string newMethodName, bool useAwait)
   {
      if (invocation is not { FirstChild: IReferenceExpression { NameIdentifier: not null } referenceExpression }) return;

      var factory = GetInstance(invocation);

      var newReferenceExpression = referenceExpression.QualifierExpression is null
                                      ? factory.CreateReferenceExpression("$0", newMethodName)
                                      : factory.CreateReferenceExpression("$0.$1", referenceExpression.QualifierExpression, newMethodName);

      newReferenceExpression.SetTypeArgumentList(referenceExpression.TypeArgumentList);

      if (useAwait)
         invocation.ReplaceBy(factory.CreateExpression("await $0($1).ConfigureAwait(false)", newReferenceExpression, invocation.ArgumentList));
      else
      {
         invocation.ReplaceBy(factory.CreateExpression(invocation.Type()
                                                                 .IsVoid()
                                                          ? "$0($1).Wait()"
                                                          : "$0($1).Result",
                                                       newReferenceExpression,
                                                       invocation.ArgumentList));
      }
   }

   #endregion
}