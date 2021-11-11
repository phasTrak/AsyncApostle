using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;

namespace AsyncApostle.AsyncHelpers.ClassSearchers;

public interface IClassSearcher
{
    #region properties

    int Priority { get; }

    #endregion

    #region methods

    [Pure]
    ITypeElement? GetClassForSearch(IParametersOwner originalMethod, IType? invokedType);

    #endregion
}