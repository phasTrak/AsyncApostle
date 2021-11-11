using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

namespace AsyncApostle.AsyncHelpers.ClassSearchers;

[SolutionComponent]
public class DefaultSearcher : IClassSearcher
{
    #region properties

    public int Priority => 0;

    #endregion

    #region methods

    public ITypeElement? GetClassForSearch(IParametersOwner originalMethod, IType? invokedType) => originalMethod.GetContainingType();

    #endregion
}