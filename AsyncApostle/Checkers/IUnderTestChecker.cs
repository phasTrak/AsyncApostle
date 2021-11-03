using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.Checkers
{
    public interface IUnderTestChecker
    {
        #region methods

        bool IsUnder(IMethodDeclaration method);

        #endregion
    }
}
