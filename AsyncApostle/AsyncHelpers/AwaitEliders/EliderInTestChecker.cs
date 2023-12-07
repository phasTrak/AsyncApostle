namespace AsyncApostle.AsyncHelpers.AwaitEliders;

[SolutionComponent]
public class EliderInTestChecker(IUnderTestChecker underTestChecker) : IConcreteAwaitEliderChecker
{
   #region methods

   public bool CanElide(IParametersOwnerDeclaration element) =>
      !element.GetSettingsStore()
              .GetValue(ExcludeTestMethodsFromEliding)
   || element is not IMethodDeclaration method
   || !underTestChecker.IsUnder(method);

   #endregion
}