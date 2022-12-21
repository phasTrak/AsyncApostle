namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;

[SolutionComponent]
class OnTestChecker : IConfigureAwaitCustomChecker
{
   #region fields

   readonly IUnderTestChecker _underTestChecker;

   #endregion

   #region constructors

   public OnTestChecker(IUnderTestChecker underTestChecker) => _underTestChecker = underTestChecker;

   #endregion

   #region methods

   public bool CanBeAdded(IAwaitExpression element) =>
      element.GetContainingTypeMemberDeclarationIgnoringClosures() is not IMethodDeclaration methodDeclaration
   || !element.GetSettingsStore()
              .GetValue(ExcludeTestMethodsFromConfigureAwait)
   || !_underTestChecker.IsUnder(methodDeclaration);

   #endregion
}