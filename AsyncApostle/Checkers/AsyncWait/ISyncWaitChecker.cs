namespace AsyncApostle.Checkers.AsyncWait;

public interface ISyncWaitChecker
{
   bool CanReplaceResultToAsync(IReferenceExpression referenceExpression);
   bool CanReplaceWaitToAsync(IInvocationExpression invocationExpression);
}