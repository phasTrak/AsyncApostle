using System.Linq;
using AsyncApostle.Helpers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.AsyncHelpers.MissingAwaitChecker;

[SolutionComponent]
public class MissingAwaitChecker : IMissingAwaitChecker
{
    /// <inheritdoc />
    public bool AwaitIsMissing(IParametersOwnerDeclaration element)
    {
        var returnStatement = element.DescendantsInScope<IReturnStatement>().FirstOrDefault();
        var invocationExpressions = element.DescendantsInScope<IInvocationExpression>()
            .ToArray();

        foreach (var invocationExpression in invocationExpressions)
        {
            var invocationType = invocationExpression.Type();
            var invocationCouldBeAwaited = invocationType.IsTask() || invocationType.IsGenericTask();
            if (!invocationCouldBeAwaited) continue;

            var invocationIsReturned = invocationExpression.GetContainingStatement() == returnStatement;
            if (!InvocationIsAwaited(invocationExpression) && !invocationIsReturned) return true;
        }

        return false;
    }

    private bool InvocationIsAwaited(ITreeNode treeNode)
    {
        if (treeNode is IAwaitExpression || treeNode.Parent is IAwaitExpression)
            return true;

        return treeNode.Parent != null && InvocationIsAwaited(treeNode.Parent);
    }
}