namespace AsyncApostle.AsyncHelpers.AwaitEliders;

public interface ICustomAwaitElider
{
   bool CanElide(ICSharpDeclaration declarationOrClosure);
   void Elide(ICSharpDeclaration declarationOrClosure, ICSharpExpression awaitExpression);
}