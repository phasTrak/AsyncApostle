using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;

namespace AsyncApostle.AsyncHelpers.MethodFinders
{
    public interface IAsyncMethodFinder
    {
        #region methods

        [Pure]
        FindingResult FindEquivalentAsyncMethod(IMethod originalMethod, IType? invokedType);

        #endregion
    }
}
