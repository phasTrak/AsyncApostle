namespace AsyncApostle.AsyncHelpers.MethodFinders;

public interface IAsyncMethodFinder
{
   [Pure] FindingResult FindEquivalentAsyncMethod(IMethod originalMethod, IType? invokedType);
}