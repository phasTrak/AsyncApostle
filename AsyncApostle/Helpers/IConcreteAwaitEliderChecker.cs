namespace AsyncApostle.Helpers;

public interface IConcreteAwaitEliderChecker
{
   bool CanElide(IParametersOwnerDeclaration element);
}