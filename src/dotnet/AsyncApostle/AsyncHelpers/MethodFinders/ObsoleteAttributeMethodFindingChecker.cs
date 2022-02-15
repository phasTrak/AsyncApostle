using JetBrains.Metadata.Reader.Impl;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

namespace AsyncApostle.AsyncHelpers.MethodFinders;

[SolutionComponent]
public class ObsoleteAttributeMethodFindingChecker : IConcreteMethodFindingChecker
{
   #region fields

   readonly ClrTypeName _obsoleteClass = new ("System.ObsoleteAttribute");

   #endregion

   #region methods

   public bool NeedSkip(IMethod originalMethod, IMethod candidateMethod) => !originalMethod.HasAttributeInstance(_obsoleteClass, false) && candidateMethod.HasAttributeInstance(_obsoleteClass, false);

   #endregion
}