using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using static System.Linq.Enumerable;

namespace AsyncApostle.Helpers
{
    public static class ParametersOwnerDeclarationExtensions
    {
        #region methods

        // TODO: already exists?
        [Pure]
        public static IEnumerable<TNode> DescendantsInScope<TNode>(this IParametersOwnerDeclaration? root) where TNode : class, ICSharpTreeNode =>
            root is null
                ? Empty<TNode>()
                : root.Descendants<TNode>()
                      .ToEnumerable()
                      .Where(x => x.GetContainingFunctionLikeDeclarationOrClosure() == root);

        #endregion
    }
}
