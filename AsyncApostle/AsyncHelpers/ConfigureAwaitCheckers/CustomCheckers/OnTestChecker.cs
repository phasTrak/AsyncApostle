namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
class OnTestChecker(IUnderTestChecker underTestChecker) : IConfigureAwaitCustomChecker
{
   #region methods

   public bool CanBeAdded(IAwaitExpression element) =>
      element.GetContainingTypeMemberDeclarationIgnoringClosures() is not IMethodDeclaration methodDeclaration
   || !element.GetSettingsStore()
              .GetValue(ExcludeTestMethodsFromConfigureAwait)
   || !underTestChecker.IsUnder(methodDeclaration);

   #endregion
}