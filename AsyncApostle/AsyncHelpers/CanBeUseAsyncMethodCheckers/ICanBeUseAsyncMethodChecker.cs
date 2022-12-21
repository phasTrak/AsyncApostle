namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers;

public interface ICanBeUseAsyncMethodChecker
{
   #region methods

   bool CanReplace(IInvocationExpression element);

   #endregion
}