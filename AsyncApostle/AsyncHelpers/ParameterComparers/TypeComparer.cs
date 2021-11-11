using AsyncApostle.Helpers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using static AsyncApostle.AsyncHelpers.ParameterComparers.ParameterCompareResultAction;

namespace AsyncApostle.AsyncHelpers.ParameterComparers;

[SolutionComponent]
class TypeComparer : ITypeComparer
{
    #region methods

    public ParameterCompareResultAction Compare(IType originalParameterType, IType parameterType) =>
        parameterType.IsEquals(originalParameterType) ? Equal : parameterType.IsAsyncDelegate(originalParameterType) ? NeedConvertToAsyncFunc : NotEqual;

    #endregion
}