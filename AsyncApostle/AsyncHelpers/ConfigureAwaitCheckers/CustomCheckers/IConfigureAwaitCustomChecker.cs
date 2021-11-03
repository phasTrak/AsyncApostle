using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers
{
    public interface IConfigureAwaitCustomChecker
    {
        #region methods

        bool CanBeAdded(IAwaitExpression element);

        #endregion
    }
}
