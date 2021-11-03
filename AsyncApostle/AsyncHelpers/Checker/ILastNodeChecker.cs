using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.AsyncHelpers.Checker
{
    public interface ILastNodeChecker
    {
        #region methods

        bool IsLastNode(ICSharpExpression element);

        #endregion
    }
}
