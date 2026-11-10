namespace AsyncApostle.Helpers;

public interface IInvocationConverter
{
   bool TryReplaceInvocationToAsync(IInvocationExpression invocationExpression);
}