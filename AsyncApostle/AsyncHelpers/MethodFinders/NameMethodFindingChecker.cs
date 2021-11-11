using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

namespace AsyncApostle.AsyncHelpers.MethodFinders;

[SolutionComponent]
public class NameMethodFindingChecker : IConcreteMethodFindingChecker
{
    #region methods

    public bool NeedSkip(IMethod originalMethod, IMethod candidateMethod) => $"{originalMethod.ShortName}Async" != candidateMethod.ShortName;

    #endregion
}