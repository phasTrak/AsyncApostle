namespace AsyncApostle.AsyncHelpers.AwaitEliders;

public interface IAwaitElider
{
   void Elide(IAwaitExpression awaitExpression);
   void Elide(IParametersOwnerDeclaration parametersOwnerDeclaration);
}