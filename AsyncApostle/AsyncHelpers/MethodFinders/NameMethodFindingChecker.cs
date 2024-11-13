namespace AsyncApostle.AsyncHelpers.MethodFinders;

[SolutionComponent(DemandAnyThreadSafe)]
public class NameMethodFindingChecker : IConcreteMethodFindingChecker
{
   #region methods

   public bool NeedSkip(IMethod originalMethod, IMethod candidateMethod) => $"{originalMethod.ShortName}Async" != candidateMethod.ShortName;

   #endregion
}