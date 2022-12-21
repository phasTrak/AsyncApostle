namespace AsyncApostle.AsyncHelpers.AwaitEliders;

public interface IAwaitElider
{
   #region methods

   void Elide(IAwaitExpression awaitExpression);
   void Elide(IParametersOwnerDeclaration parametersOwnerDeclaration);

   #endregion
}