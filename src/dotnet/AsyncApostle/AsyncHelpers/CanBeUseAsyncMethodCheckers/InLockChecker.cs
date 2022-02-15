using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers;

[SolutionComponent]
class InLockChecker : IConcreteCanBeUseAsyncMethodChecker
{
   #region methods

   public bool CanReplace(IInvocationExpression element) => element.GetContainingNode<ILockStatement>() is null;

   #endregion
}