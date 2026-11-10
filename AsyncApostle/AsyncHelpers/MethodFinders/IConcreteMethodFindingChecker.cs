namespace AsyncApostle.AsyncHelpers.MethodFinders;

public interface IConcreteMethodFindingChecker
{
   bool NeedSkip(IMethod originalMethod, IMethod candidateMethod);
}