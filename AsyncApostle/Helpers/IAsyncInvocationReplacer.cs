namespace AsyncApostle.Helpers;

public interface IAsyncInvocationReplacer
{
   #region methods

   void ReplaceInvocation(IInvocationExpression? invocation, string newMethodName, bool useAwait);

   #endregion
}