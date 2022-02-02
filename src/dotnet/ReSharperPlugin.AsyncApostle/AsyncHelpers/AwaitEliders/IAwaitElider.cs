using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.AsyncHelpers.AwaitEliders;

public interface IAwaitElider
{
   #region methods

   void Elide(IAwaitExpression awaitExpression);
   void Elide(IParametersOwnerDeclaration parametersOwnerDeclaration);

   #endregion
}