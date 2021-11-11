using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;

namespace AsyncApostle.AsyncHelpers.ClassSearchers;

public interface IClassForSearchResolver
{
    #region methods

    [Pure]
    ITypeElement? GetClassForSearch(IParametersOwner originalMethod, IType? invokedType);

    #endregion
}