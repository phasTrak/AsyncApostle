using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.Helpers;

public interface IInvocationConverter
{
   #region methods

   bool TryReplaceInvocationToAsync(IInvocationExpression invocationExpression);

   #endregion
}