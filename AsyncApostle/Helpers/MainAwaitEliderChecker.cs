namespace AsyncApostle.Helpers;

[SolutionComponent(DemandAnyThreadSafe)]
public class MainAwaitEliderChecker(ILastNodeChecker lastNodeChecker) : IConcreteAwaitEliderChecker
{
   #region methods

   public bool CanElide(IParametersOwnerDeclaration element)
   {
      var returnType = element.DeclaredParametersOwner?.ReturnType;

      if (returnType is null) return false;

      IReturnStatement[] returnStatements = [..element.DescendantsInScope<IReturnStatement>()];

      if (returnType.IsTask() && returnStatements.Any() || returnType.IsGenericTask() && returnStatements.Length > 1) return false;

      IAwaitExpression[] awaitExpressions = [..element.DescendantsInScope<IAwaitExpression>()];

      // TODO: think about this, different settings
      if (awaitExpressions.Length is not 1) return false;

      var awaitExpression = awaitExpressions[0];

      if (returnStatements.Any() && returnStatements[0] != awaitExpression.GetContainingStatement() || !lastNodeChecker.IsLastNode(awaitExpression)) return false;

      var awaitingType = (awaitExpression.Task as IInvocationExpression)?.RemoveConfigureAwait()
                                                                         .Type();

      return awaitingType is not null && (awaitingType.Equals(returnType) || returnType.IsTask() && awaitingType.IsGenericTask() || returnType.IsVoid());
   }

   #endregion
}