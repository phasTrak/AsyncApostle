namespace AsyncApostle.AsyncHelpers.AwaitEliders;

[SolutionComponent(DemandAnyThreadSafe)]
public class EliderInTestChecker(IUnderTestChecker underTestChecker) : IConcreteAwaitEliderChecker
{
   #region methods

   public bool CanElide(IParametersOwnerDeclaration element) =>
      !element.GetSettingsStore()
              .GetKey<GeneralSettings>(SettingsOptimization.OptimizeDefault)
              .ExcludeTestMethodsFromEliding
   || element is not IMethodDeclaration method
   || !underTestChecker.IsUnder(method);

   #endregion
}