using System.Collections.Generic;
using System.Linq;
using AsyncApostle.Helpers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.AsyncHelpers.AwaitEliders;

[SolutionComponent]
class AwaitElider : IAwaitElider
{
   #region fields

   readonly ICustomAwaitElider[] _awaitEliders;

   #endregion

   #region constructors

   public AwaitElider(IEnumerable<ICustomAwaitElider> awaitEliders) => _awaitEliders = awaitEliders.ToArray();

   #endregion

   #region methods

   public void Elide(IAwaitExpression awaitExpression)
   {
      if (awaitExpression.Task is not IInvocationExpression invocationExpression) return;

      var declarationOrClosure = awaitExpression.GetContainingFunctionLikeDeclarationOrClosure();

      if (declarationOrClosure is null) return;

      _awaitEliders.FirstOrDefault(x => x.CanElide(declarationOrClosure))
                  ?.Elide(declarationOrClosure, invocationExpression.RemoveConfigureAwait());
   }

   public void Elide(IParametersOwnerDeclaration parametersOwnerDeclaration)
   {
      foreach (var awaitExpression in parametersOwnerDeclaration.DescendantsInScope<IAwaitExpression>()) Elide(awaitExpression);
   }

   #endregion
}