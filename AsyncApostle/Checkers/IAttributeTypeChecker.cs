namespace AsyncApostle.Checkers;

public interface IAttributeTypeChecker
{
   bool IsUnder(ICSharpTreeNode node);
}