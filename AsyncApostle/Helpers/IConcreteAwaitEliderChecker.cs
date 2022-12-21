namespace AsyncApostle.Helpers;

public interface IConcreteAwaitEliderChecker
{
   #region methods

   bool CanElide(IParametersOwnerDeclaration element);

   #endregion
}