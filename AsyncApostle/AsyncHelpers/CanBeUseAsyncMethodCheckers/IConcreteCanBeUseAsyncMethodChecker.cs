namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers;

public interface IConcreteCanBeUseAsyncMethodChecker
{
   bool CanReplace(IInvocationExpression element);
}