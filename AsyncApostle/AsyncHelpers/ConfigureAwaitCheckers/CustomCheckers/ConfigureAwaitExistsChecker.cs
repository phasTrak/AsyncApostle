namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;

[SolutionComponent]
public class ConfigureAwaitExistsChecker : IConfigureAwaitCustomChecker
{
   #region methods

   public bool CanBeAdded(IAwaitExpression element)
   {
      var type = element.Task.Type();

      return type.IsTask()
          || type.IsGenericTask()
          || ((type as IDeclaredType)?.Resolve()
                                      .DeclaredElement as ITypeElement)?.Methods.Any(static x => x.ShortName is "ConfigureAwait" && x.Parameters.Count is 1) is true;
   }

   #endregion
}