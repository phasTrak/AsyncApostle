﻿namespace AsyncApostle.AsyncHelpers.AwaitEliders;

[SolutionComponent(DemandAnyThreadSafe)]
class UsingVarChecker : IConcreteAwaitEliderChecker
{
   #region methods

   public bool CanElide(IParametersOwnerDeclaration element) =>
      element.Descendants<IMultipleLocalVariableDeclaration>()
             .ToEnumerable()
             .All(static x => x.UsingKind is Regular);

   #endregion
}