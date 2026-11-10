namespace AsyncApostle.AsyncHelpers.AwaitEliders;

public interface IAwaitEliderChecker
{
   bool CanElide(IParametersOwnerDeclaration element);
}