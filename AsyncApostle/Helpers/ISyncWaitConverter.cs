namespace AsyncApostle.Helpers;

public interface ISyncWaitConverter
{
   void ReplaceResultToAsync(IReferenceExpression referenceExpression);
   void ReplaceWaitToAsync(IInvocationExpression invocationExpression);
}