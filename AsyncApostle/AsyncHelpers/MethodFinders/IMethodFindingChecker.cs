using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;

namespace AsyncApostle.AsyncHelpers.MethodFinders;

public interface IMethodFindingChecker
{
    #region methods

    [Pure]
    bool NeedSkip(IMethod originalMethod, IMethod candidateMethod);

    #endregion
}