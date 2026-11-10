namespace AsyncApostle.AsyncHelpers.AwaitEliders;

[SolutionComponent(DemandAnyThreadSafe)]
public class AwaitEliderChecker(IEnumerable<IConcreteAwaitEliderChecker> checkers) : IAwaitEliderChecker
{
   readonly IConcreteAwaitEliderChecker[] _checkers = checkers as IConcreteAwaitEliderChecker[] ?? [.. checkers];
   public bool CanElide(IParametersOwnerDeclaration element) => TrueForAll(_checkers, x => x.CanElide(element));
}