namespace AsyncApostle.Checkers;

public interface IAttributeTypeChecker
{
   #region methods

   bool IsUnder(ICSharpTreeNode node);

   #endregion
}