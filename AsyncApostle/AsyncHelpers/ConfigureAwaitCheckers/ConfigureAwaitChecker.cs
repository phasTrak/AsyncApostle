namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
class ConfigureAwaitChecker(IEnumerable<IConfigureAwaitCustomChecker> awaitCustomCheckers) : IConfigureAwaitChecker
{
   readonly IConfigureAwaitCustomChecker[] _awaitCustomCheckers = awaitCustomCheckers as IConfigureAwaitCustomChecker[] ?? [.. awaitCustomCheckers];
   public bool NeedAdding(IAwaitExpression element) => TrueForAll(_awaitCustomCheckers, x => x.CanBeAdded(element));
}