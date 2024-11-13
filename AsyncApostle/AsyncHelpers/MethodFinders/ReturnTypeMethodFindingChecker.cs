namespace AsyncApostle.AsyncHelpers.MethodFinders;

[SolutionComponent(DemandAnyThreadSafe)]
public class ReturnTypeMethodFindingChecker : IConcreteMethodFindingChecker
{
   #region methods

   public bool NeedSkip(IMethod originalMethod, IMethod candidateMethod)
   {
      var originalReturnType = originalMethod.Type();

      return originalReturnType is not null
          && candidateMethod.Type()
                           ?.IsTaskOf(originalReturnType) is not true;
   }

   #endregion
}