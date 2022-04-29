using System.Linq;
using AsyncApostle.AsyncHelpers.MethodFinders;
using AsyncApostle.AsyncHelpers.ParameterComparers;
using AsyncApostle.Checkers.AsyncWait;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using static AsyncApostle.AsyncHelpers.ParameterComparers.ParameterCompareResultAction;

namespace AsyncApostle.Helpers;

[SolutionComponent]
public class InvocationConverter : IInvocationConverter
{
   #region fields

   readonly IAsyncInvocationReplacer _asyncInvocationReplacer;
   readonly IAsyncMethodFinder       _asyncMethodFinder;
   readonly ISyncWaitChecker         _syncWaitChecker;
   readonly ISyncWaitConverter       _syncWaitConverter;

   #endregion

   #region constructors

   public InvocationConverter(IAsyncMethodFinder asyncMethodFinder,
                              IAsyncInvocationReplacer asyncInvocationReplacer,
                              ISyncWaitChecker syncWaitChecker,
                              ISyncWaitConverter syncWaitConverter) =>
      (_asyncInvocationReplacer, _asyncMethodFinder, _syncWaitChecker, _syncWaitConverter) = (asyncInvocationReplacer, asyncMethodFinder, syncWaitChecker, syncWaitConverter);

   #endregion

   #region methods

   public bool TryReplaceInvocationToAsync(IInvocationExpression invocationExpression)
   {
      var referenceCurrentResolveResult = invocationExpression.Reference.Resolve();

      if (!referenceCurrentResolveResult.IsValid() || referenceCurrentResolveResult.DeclaredElement is not IMethod invocationMethod) return false;

      var findingResult = _asyncMethodFinder.FindEquivalentAsyncMethod(invocationMethod, (invocationExpression.ConditionalQualifier as IReferenceExpression)?.QualifierExpression?.Type());

      if (!findingResult.CanBeConvertedToAsync() || !TryConvertParameterFuncToAsync(invocationExpression, findingResult.ParameterCompareResult) || findingResult.Method is null) return false;

      _asyncInvocationReplacer.ReplaceInvocation(invocationExpression, findingResult.Method.ShortName, true);

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
            if (parameterCompareResult.ParameterResults[i]
                                      .Action is not NeedConvertToAsyncFunc)
               continue;

            if (invocationExpression.Arguments[i]
                                    .Value is not ILambdaExpression lambdaExpression)
            {
               invocationExpression.PsiModule.GetPsiServices()
                                   .Transactions.RollbackTransaction();

               return false;
            }

            lambdaExpression.SetAsync(true);

            while (lambdaExpression.DescendantsInScope<IInvocationExpression>()
                                   .FirstOrDefault(_syncWaitChecker.CanReplaceWaitToAsync) is { } innerInvocationExpression)
               _syncWaitConverter.ReplaceWaitToAsync(innerInvocationExpression);

            while (lambdaExpression.DescendantsInScope<IReferenceExpression>()
                                   .FirstOrDefault(_syncWaitChecker.CanReplaceResultToAsync) is { } referenceExpression)
               _syncWaitConverter.ReplaceResultToAsync(referenceExpression);

            foreach (var unused in lambdaExpression.DescendantsInScope<IInvocationExpression>()
                                                   .Where(innerInvocationExpression2 => !TryReplaceInvocationToAsync(innerInvocationExpression2)))
            {
               //invocationExpression.PsiModule.GetPsiServices().Transactions.RollbackTransaction();
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