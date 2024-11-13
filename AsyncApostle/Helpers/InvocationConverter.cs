namespace AsyncApostle.Helpers;

[SolutionComponent(DemandAnyThreadSafe)]
public class InvocationConverter(IAsyncMethodFinder asyncMethodFinder,
                                 IAsyncInvocationReplacer asyncInvocationReplacer,
                                 ISyncWaitChecker syncWaitChecker,
                                 ISyncWaitConverter syncWaitConverter) : IInvocationConverter
{
   #region methods

   public bool TryReplaceInvocationToAsync(IInvocationExpression invocationExpression)
   {
      var referenceCurrentResolveResult = invocationExpression.Reference.Resolve();

      if (!referenceCurrentResolveResult.IsValid() || referenceCurrentResolveResult.DeclaredElement is not IMethod invocationMethod) return false;

      var findingResult = asyncMethodFinder.FindEquivalentAsyncMethod(invocationMethod, (invocationExpression.ConditionalQualifier as IReferenceExpression)?.QualifierExpression?.Type());

      if (!findingResult.CanBeConvertedToAsync() || !TryConvertParameterFuncToAsync(invocationExpression, findingResult.ParameterCompareResult) || findingResult.Method is null) return false;

      asyncInvocationReplacer.ReplaceInvocation(invocationExpression, findingResult.Method.ShortName, true);

      return true;
   }

   bool TryConvertParameterFuncToAsync(ICSharpArgumentsOwner invocationExpression, ParameterCompareResult parameterCompareResult)
   {
      invocationExpression.PsiModule.GetPsiServices()
                          .Transactions.StartTransaction("convertAsyncParameter");

      try
      {
         for (var i = 0; i < invocationExpression.Arguments.Count; i++)
         {
            if (parameterCompareResult.ParameterResults[i].Action is not NeedConvertToAsyncFunc) continue;

            if (invocationExpression.Arguments[i].Value is not ILambdaExpression lambdaExpression)
            {
               invocationExpression.PsiModule.GetPsiServices()
                                   .Transactions.RollbackTransaction();

               return false;
            }

            lambdaExpression.SetAsync(true);

            while (lambdaExpression.DescendantsInScope<IInvocationExpression>()
                                   .FirstOrDefault(syncWaitChecker.CanReplaceWaitToAsync) is { } innerInvocationExpression)
               syncWaitConverter.ReplaceWaitToAsync(innerInvocationExpression);

            while (lambdaExpression.DescendantsInScope<IReferenceExpression>()
                                   .FirstOrDefault(syncWaitChecker.CanReplaceResultToAsync) is { } referenceExpression)
               syncWaitConverter.ReplaceResultToAsync(referenceExpression);

            foreach (var unused in lambdaExpression.DescendantsInScope<IInvocationExpression>()
                                                   .Where(innerInvocationExpression2 => !TryReplaceInvocationToAsync(innerInvocationExpression2)))
            {
               // block intentionally left empty
            }
         }
      }
      catch
      {
         invocationExpression.PsiModule.GetPsiServices()
                             .Transactions.RollbackTransaction();

         return false;
      }

      invocationExpression.PsiModule.GetPsiServices()
                          .Transactions.CommitTransaction();

      return true;
   }

   #endregion
}