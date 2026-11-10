namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;

public interface IConfigureAwaitCustomChecker
{
   bool CanBeAdded(IAwaitExpression element);
}