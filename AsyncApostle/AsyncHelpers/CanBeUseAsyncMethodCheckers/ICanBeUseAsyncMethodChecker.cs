namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers;

public interface ICanBeUseAsyncMethodChecker
{
   bool CanReplace(IInvocationExpression element);
}