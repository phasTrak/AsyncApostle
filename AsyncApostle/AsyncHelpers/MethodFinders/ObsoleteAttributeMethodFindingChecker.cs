namespace AsyncApostle.AsyncHelpers.MethodFinders;

[SolutionComponent(DemandAnyThreadSafe)]
public class ObsoleteAttributeMethodFindingChecker : IConcreteMethodFindingChecker
{
   readonly ClrTypeName _obsoleteClass = new ("System.ObsoleteAttribute");
   public bool NeedSkip(IMethod originalMethod, IMethod candidateMethod) => !originalMethod.HasAttributeInstance(_obsoleteClass, false) && candidateMethod.HasAttributeInstance(_obsoleteClass, false);
}