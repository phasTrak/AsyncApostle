namespace AsyncApostle.Helpers;

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