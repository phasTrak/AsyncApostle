using System.Linq;
using AsyncApostle.AsyncHelpers;
using AsyncApostle.AsyncHelpers.Checker;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.Helpers;

[SolutionComponent]
public class MainAwaitEliderChecker : IConcreteAwaitEliderChecker
{
   #region fields

   readonly ILastNodeChecker _lastNodeChecker;

   #endregion

   #region constructors

   public MainAwaitEliderChecker(ILastNodeChecker lastNodeChecker) => _lastNodeChecker = lastNodeChecker;

   #endregion

   #region methods

   public bool CanElide(IParametersOwnerDeclaration element)
   {
      var returnType = element.DeclaredParametersOwner?.ReturnType;

      if (returnType is null) return false;

      var returnStatements = element.DescendantsInScope<IReturnStatement>()
                                    .ToArray();

      if (returnType.IsTask() && returnStatements.Any() || returnType.IsGenericTask() && returnStatements.Length > 1) return false;

      var awaitExpressions = element.DescendantsInScope<IAwaitExpression>()
                                    .ToArray();

      // TODO: think about this, different settings
      if (awaitExpressions.Length is not 1) return false;

      var awaitExpression = awaitExpressions.First();

      if (returnStatements.Any() && returnStatements.First() != awaitExpression.GetContainingStatement() || !_lastNodeChecker.IsLastNode(awaitExpression)) return false;

      var awaitingType = (awaitExpression.Task as IInvocationExpression)?.RemoveConfigureAwait()
                                                                         .Type();

      return awaitingType is not null && (awaitingType.Equals(returnType) || returnType.IsTask() && awaitingType.IsGenericTask() || returnType.IsVoid());
   }

   #endregion
}