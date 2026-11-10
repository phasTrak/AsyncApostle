namespace AsyncApostle.Helpers;

public interface IAsyncInvocationReplacer
{
   void ReplaceInvocation(IInvocationExpression? invocation, string newMethodName, bool useAwait);
}