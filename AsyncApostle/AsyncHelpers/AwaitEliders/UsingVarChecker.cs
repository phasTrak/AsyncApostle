namespace AsyncApostle.AsyncHelpers.AwaitEliders;

[SolutionComponent(DemandAnyThreadSafe)]
class UsingVarChecker : IConcreteAwaitEliderChecker
{
   public bool CanElide(IParametersOwnerDeclaration element) =>
      element.Descendants<IMultipleLocalVariableDeclaration>()
             .ToEnumerable()
             .All(static x => x.UsingKind is Regular);
}