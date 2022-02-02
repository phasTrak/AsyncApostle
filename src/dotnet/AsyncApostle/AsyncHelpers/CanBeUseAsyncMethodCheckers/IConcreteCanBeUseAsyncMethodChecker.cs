using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers;

public interface IConcreteCanBeUseAsyncMethodChecker
{
   #region methods

   bool CanReplace(IInvocationExpression element);

   #endregion
}