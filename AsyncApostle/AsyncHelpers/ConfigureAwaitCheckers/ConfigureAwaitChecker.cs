namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers;

[SolutionComponent]
class ConfigureAwaitChecker(IEnumerable<IConfigureAwaitCustomChecker> awaitCustomCheckers) : IConfigureAwaitChecker
{
   #region fields

   readonly IConfigureAwaitCustomChecker[] _awaitCustomCheckers = awaitCustomCheckers as IConfigureAwaitCustomChecker[] ?? [..awaitCustomCheckers];

   #endregion

   #region methods

   public bool NeedAdding(IAwaitExpression element) => TrueForAll(_awaitCustomCheckers, x => x.CanBeAdded(element));

   #endregion
}