using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.AsyncHelpers.Checker;

public interface IAttributeFunctionChecker
{
   #region methods

   bool IsUnder(ICSharpTreeNode node);

   #endregion
}