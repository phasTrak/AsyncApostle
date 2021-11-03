using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.AsyncHelpers.AwaitEliders
{
    public interface ICustomAwaitElider
    {
        #region methods

        bool CanElide(ICSharpDeclaration declarationOrClosure);
        void Elide(ICSharpDeclaration declarationOrClosure, ICSharpExpression awaitExpression);

        #endregion
    }
}
