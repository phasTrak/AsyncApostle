using JetBrains.ReSharper.Psi;

namespace AsyncApostle.Helpers;

public interface IAsyncReplacer
{
    #region methods

    void ReplaceToAsync(IMethod methodDeclaredElement);

    #endregion
}