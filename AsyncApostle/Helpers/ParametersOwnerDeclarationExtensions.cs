namespace AsyncApostle.Helpers;

public static class ParametersOwnerDeclarationExtensions
{
   // TODO: already exists?
   [Pure]
   public static IEnumerable<TNode> DescendantsInScope<TNode>(this IParametersOwnerDeclaration? root) where TNode : class, ICSharpTreeNode =>
      root is null
         ? []
         : root.Descendants<TNode>()
               .ToEnumerable()
               .Where(x => x.GetContainingFunctionLikeDeclarationOrClosure() == root);
}