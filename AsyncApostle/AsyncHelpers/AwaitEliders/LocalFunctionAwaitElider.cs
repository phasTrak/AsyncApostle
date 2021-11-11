using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using static JetBrains.ReSharper.Psi.CSharp.CSharpElementFactory;

namespace AsyncApostle.AsyncHelpers.AwaitEliders;

[SolutionComponent]
class LocalFunctionAwaitElider : ICustomAwaitElider
{
    #region methods

    public bool CanElide(ICSharpDeclaration declarationOrClosure) => declarationOrClosure is ILocalFunctionDeclaration;

    public void Elide(ICSharpDeclaration declarationOrClosure, ICSharpExpression awaitExpression)
    {
        if (declarationOrClosure is not ILocalFunctionDeclaration localFunctionDeclaration)
            return;

        localFunctionDeclaration.SetAsync(false);

        if (localFunctionDeclaration.Body is not null)
        {
            awaitExpression.GetContainingStatement()
                           ?.ReplaceBy(GetInstance(awaitExpression)
                                           .CreateStatement("return $0;", awaitExpression));
        }
        else
            localFunctionDeclaration.ArrowClause?.SetExpression(awaitExpression);
    }

    #endregion
}