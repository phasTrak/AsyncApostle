namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers;

public interface IConfigureAwaitChecker
{
   #region methods

   bool NeedAdding(IAwaitExpression element);

   #endregion
}