namespace AsyncApostle.AsyncHelpers.ParameterComparers;

[SolutionComponent(DemandAnyThreadSafe)]
class TypeComparer : ITypeComparer
{
   #region methods

   public ParameterCompareResultAction Compare(IType originalParameterType, IType parameterType) =>
      parameterType.IsEquals(originalParameterType)        ? ParameterCompareResultAction.Equal :
      parameterType.IsAsyncDelegate(originalParameterType) ? NeedConvertToAsyncFunc : ParameterCompareResultAction.NotEqual;

   #endregion
}