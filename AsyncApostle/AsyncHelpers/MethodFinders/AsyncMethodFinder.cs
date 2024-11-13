namespace AsyncApostle.AsyncHelpers.MethodFinders;

[SolutionComponent(DemandAnyThreadSafe)]
public class AsyncMethodFinder(IClassForSearchResolver classForSearchResolver, IParameterComparer parameterComparer, IMethodFindingChecker methodFindingChecker) : IAsyncMethodFinder
{
   #region methods

   public FindingResult FindEquivalentAsyncMethod(IMethod originalMethod, IType? invokedType)
   {
      if (!originalMethod.IsValid()) return CreateFail();

      var @class = classForSearchResolver.GetClassForSearch(originalMethod, invokedType);

      if (@class is null) return CreateFail();

      foreach (var candidateMethod in @class.Methods)
      {
         if (methodFindingChecker.NeedSkip(originalMethod, candidateMethod)) continue;

         var parameterCompareResult = parameterComparer.ComparerParameters(candidateMethod.Parameters, originalMethod.Parameters);

         if (!parameterCompareResult.CanBeConvertedToAsync()) continue;

         return new ()
                {
                   Method                 = candidateMethod,
                   ParameterCompareResult = parameterCompareResult
                };
      }

      return CreateFail();
   }

   #endregion
}