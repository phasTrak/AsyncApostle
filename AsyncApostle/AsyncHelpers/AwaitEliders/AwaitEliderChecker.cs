namespace AsyncApostle.AsyncHelpers.AwaitEliders;

[SolutionComponent]
public class AwaitEliderChecker(IEnumerable<IConcreteAwaitEliderChecker> checkers) : IAwaitEliderChecker
{
   #region fields

   readonly IConcreteAwaitEliderChecker[] _checkers = checkers as IConcreteAwaitEliderChecker[] ?? [..checkers];

   #endregion

   #region methods

   public bool CanElide(IParametersOwnerDeclaration element) => TrueForAll(_checkers, x => x.CanElide(element));

   #endregion
}