namespace AsyncApostle.AsyncHelpers.MethodFinders;

[SolutionComponent(DemandAnyThreadSafe)]
public class MethodFindingChecker(IEnumerable<IConcreteMethodFindingChecker> concreteMethodFindingCheckers) : IMethodFindingChecker
{
   readonly IConcreteMethodFindingChecker[] _concreteMethodFindingCheckers = concreteMethodFindingCheckers as IConcreteMethodFindingChecker[] ?? [.. concreteMethodFindingCheckers];
   public bool NeedSkip(IMethod originalMethod, IMethod candidateMethod) => Exists(_concreteMethodFindingCheckers, x => x.NeedSkip(originalMethod, candidateMethod));
}