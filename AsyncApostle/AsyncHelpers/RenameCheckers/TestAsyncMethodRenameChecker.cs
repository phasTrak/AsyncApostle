namespace AsyncApostle.AsyncHelpers.RenameCheckers;

[SolutionComponent(DemandAnyThreadSafe)]
class TestRenameChecker(IUnderTestChecker underTestChecker) : IConcreteRenameChecker
{
   #region methods

   public bool SkipRename(IMethodDeclaration methodDeclaration) =>
      methodDeclaration.GetSettingsStore()
                       .GetKey<GeneralSettings>(SettingsOptimization.OptimizeDefault)
                       .ExcludeTestMethodsFromRenaming
   && underTestChecker.IsUnder(methodDeclaration);

   #endregion
}