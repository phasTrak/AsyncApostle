using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.AsyncHelpers.ParameterComparers;

public static class AsyncExtensions
{
    #region methods

    [Pure]
    public static bool IsUnderAsync(this ITreeNode node)
    {
        foreach (var containingNode in node.ContainingNodes())
        {
            switch (containingNode)
            {
                case IMethodDeclaration methodDeclaration:
                    return methodDeclaration.Type.IsTask() || methodDeclaration.Type.IsGenericTask();
                case IAnonymousFunctionExpression functionExpression:
                    return functionExpression.InferredReturnType.IsTask() || functionExpression.InferredReturnType.IsGenericTask();
                case ILocalFunctionDeclaration functionDeclaration:
                    return functionDeclaration.Type.IsTask() || functionDeclaration.Type.IsGenericTask();
                case IQueryParameterPlatform:
                case ICSharpTypeMemberDeclaration:
                    return false;
            }
        }

        return false;
    }

    #endregion
}