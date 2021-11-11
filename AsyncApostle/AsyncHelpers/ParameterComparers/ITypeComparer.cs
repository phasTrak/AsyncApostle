using JetBrains.ReSharper.Psi;

namespace AsyncApostle.AsyncHelpers.ParameterComparers;

public interface ITypeComparer
{
    #region methods

    ParameterCompareResultAction Compare(IType originalParameterType, IType parameterType);

    #endregion
}