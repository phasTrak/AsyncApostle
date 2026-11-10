namespace AsyncApostle.AsyncHelpers.MethodFinders;

public interface IMethodFindingChecker
{
   [Pure] bool NeedSkip(IMethod originalMethod, IMethod candidateMethod);
}