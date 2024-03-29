namespace AsyncApostle.Checkers.AsyncWait;

public interface ISyncWaitChecker
{
   #region methods

   bool CanReplaceResultToAsync(IReferenceExpression referenceExpression);
   bool CanReplaceWaitToAsync(IInvocationExpression invocationExpression);

   #endregion
}