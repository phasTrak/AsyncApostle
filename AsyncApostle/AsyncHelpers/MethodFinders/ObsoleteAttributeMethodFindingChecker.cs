namespace AsyncApostle.AsyncHelpers.MethodFinders;

[SolutionComponent(DemandAnyThreadSafe)]
public class ObsoleteAttributeMethodFindingChecker : IConcreteMethodFindingChecker
{
   #region fields

   readonly ClrTypeName _obsoleteClass = new ("System.ObsoleteAttribute");

   #endregion

   #region methods

   public bool NeedSkip(IMethod originalMethod, IMethod candidateMethod) => !originalMethod.HasAttributeInstance(_obsoleteClass, false) && candidateMethod.HasAttributeInstance(_obsoleteClass, false);

   #endregion
}