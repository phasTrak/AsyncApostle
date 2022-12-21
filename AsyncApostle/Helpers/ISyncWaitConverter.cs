namespace AsyncApostle.Helpers;

public interface ISyncWaitConverter
{
   #region methods

   void ReplaceResultToAsync(IReferenceExpression referenceExpression);
   void ReplaceWaitToAsync(IInvocationExpression invocationExpression);

   #endregion
}