namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers;

public interface IConfigureAwaitChecker
{
   bool NeedAdding(IAwaitExpression element);
}