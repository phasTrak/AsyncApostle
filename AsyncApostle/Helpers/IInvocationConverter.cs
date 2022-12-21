namespace AsyncApostle.Helpers;

public interface IInvocationConverter
{
   #region methods

   bool TryReplaceInvocationToAsync(IInvocationExpression invocationExpression);

   #endregion
}