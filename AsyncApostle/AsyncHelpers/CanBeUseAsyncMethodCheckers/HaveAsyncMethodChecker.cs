namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers;

[SolutionComponent]
class HaveAsyncMethodChecker(IAsyncMethodFinder asyncMethodFinder) : IConcreteCanBeUseAsyncMethodChecker
{
   #region methods

   public bool CanReplace(IInvocationExpression element)
   {
      var referenceCurrentResolveResult = element.Reference.Resolve();

      return referenceCurrentResolveResult.IsValid()
          && referenceCurrentResolveResult.DeclaredElement is IMethod invocationMethod
          && asyncMethodFinder.FindEquivalentAsyncMethod(invocationMethod, (element.ConditionalQualifier as IReferenceExpression)?.QualifierExpression?.Type())
                              .ParameterCompareResult.CanBeConvertedToAsync();
   }

   #endregion
}