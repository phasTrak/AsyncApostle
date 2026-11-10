namespace AsyncApostle.Checkers;

public interface IUnderTestChecker
{
   bool IsUnder(IMethodDeclaration method);
}