using JetBrains.ReSharper.Psi;

namespace AsyncApostle.AsyncHelpers.MethodFinders;

public interface IConcreteMethodFindingChecker
{
   #region methods

   bool NeedSkip(IMethod originalMethod, IMethod candidateMethod);

   #endregion
}