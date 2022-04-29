using AsyncApostle.AsyncHelpers.ClassSearchers;
using AsyncApostle.AsyncHelpers.ParameterComparers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using static AsyncApostle.AsyncHelpers.MethodFinders.FindingResult;

namespace AsyncApostle.AsyncHelpers.MethodFinders;

[SolutionComponent]
public class AsyncMethodFinder : IAsyncMethodFinder
{
   #region fields

   readonly IClassForSearchResolver _classForSearchResolver;
   readonly IMethodFindingChecker   _methodFindingChecker;
   readonly IParameterComparer      _parameterComparer;

   #endregion

   #region constructors

   public AsyncMethodFinder(IClassForSearchResolver classForSearchResolver, IParameterComparer parameterComparer, IMethodFindingChecker methodFindingChecker) =>
      (_classForSearchResolver, _methodFindingChecker, _parameterComparer) = (classForSearchResolver, methodFindingChecker, parameterComparer);

   #endregion

   #region methods

   public FindingResult FindEquivalentAsyncMethod(IMethod originalMethod, IType? invokedType)
   {
      if (!originalMethod.IsValid()) return CreateFail();

      var @class = _classForSearchResolver.GetClassForSearch(originalMethod, invokedType);

      if (@class is null) return CreateFail();

      foreach (var candidateMethod in @class.Methods)
      {
         if (_methodFindingChecker.NeedSkip(originalMethod, candidateMethod)) continue;

         var parameterCompareResult = _parameterComparer.ComparerParameters(candidateMethod.Parameters, originalMethod.Parameters);

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