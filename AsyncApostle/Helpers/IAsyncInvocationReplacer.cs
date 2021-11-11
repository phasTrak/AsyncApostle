using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.Helpers;

public interface IAsyncInvocationReplacer
{
    #region methods

    void ReplaceInvocation(IInvocationExpression? invocation, string newMethodName, bool useAwait);

    #endregion
}