using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.Helpers;

public interface IConcreteAwaitEliderChecker
{
    #region methods

    bool CanElide(IParametersOwnerDeclaration element);

    #endregion
}